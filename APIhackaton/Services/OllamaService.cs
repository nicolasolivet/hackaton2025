using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System;


namespace APIhackaton.Services
{
    public class OllamaService
    {
        private readonly HttpClient _httpClient;

        public OllamaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Send the prompt to Ollama's chat completions endpoint.
        // Strategy: 1) fetch available models and pick the first; 2) call /v1/chat/completions
        public async Task<string> SendPromptAsync(string prompt)
        {
            // 1) Get models
            var modelsResp = await _httpClient.GetAsync("/v1/models");
            string modelsBody = await modelsResp.Content.ReadAsStringAsync();
            if (!modelsResp.IsSuccessStatusCode)
            {
                // Try a diagnostic fallback: maybe server exposes a different API path
                // Provide a clear exception containing the status and body to help debugging.
                throw new HttpRequestException($"GET /v1/models returned {(int)modelsResp.StatusCode}: {modelsBody}");
            }

            using var modelsDoc = JsonDocument.Parse(modelsBody);

            string modelId = null;
            if (modelsDoc.RootElement.TryGetProperty("data", out var data) && data.GetArrayLength() > 0)
            {
                modelId = data[0].GetProperty("id").GetString();
            }

            if (string.IsNullOrEmpty(modelId))
            {
                throw new InvalidOperationException("No Ollama models available");
            }

            // 2) Build chat completion request
            var requestObj = new
            {
                model = modelId,
                messages = new[] { new { role = "user", content = prompt } }
            };

            var payload = JsonSerializer.Serialize(requestObj);
            using var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/v1/chat/completions", content);
            var respText = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                // If 404, include response body to help identify correct path
                throw new HttpRequestException($"POST /v1/chat/completions returned {(int)response.StatusCode}: {respText}");
            }

            try
            {
                using var doc = JsonDocument.Parse(respText);
                if (doc.RootElement.TryGetProperty("choices", out var choices) && choices.GetArrayLength() > 0)
                {
                    var first = choices[0];
                    if (first.TryGetProperty("message", out var message) && message.TryGetProperty("content", out var contentProp))
                    {
                        return contentProp.GetString();
                    }
                }
            }
            catch (JsonException)
            {
                // fall through and return raw response
            }

            return respText;
        }
    }
}
