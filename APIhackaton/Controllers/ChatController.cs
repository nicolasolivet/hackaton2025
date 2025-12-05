using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APIhackaton.Services;

namespace APIhackaton.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly OllamaService _ollama;

        public ChatController(OllamaService ollama)
        {
            _ollama = ollama;
        }

        public class AskRequest { public string Prompt { get; set; } }
        public class AskResponse { public string Reply { get; set; } }

        [HttpPost("ask")]
        public async Task<IActionResult> Ask([FromBody] AskRequest req)
        {
            if (req == null || string.IsNullOrWhiteSpace(req.Prompt))
                return BadRequest(new { error = "Prompt is required" });

            try
            {
                var raw = await _ollama.SendPromptAsync(req.Prompt);
                // raw currently returns the body from Ollama; adapt as needed.
                return Ok(new AskResponse { Reply = raw });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
