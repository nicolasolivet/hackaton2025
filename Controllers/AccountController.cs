using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class AccountController : Controller
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string returnUrl = "/")
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(string username, string password, string returnUrl = "/")
    {
        // Aquí va la lógica de autenticación real (Identity o custom).
        // Esto es solo ejemplo para redirigir cuando "logea".
        if (username == "test" && password == "test")
        {
            // hacer sign-in real con ClaimsPrincipal
            return LocalRedirect(returnUrl);
        }

        ModelState.AddModelError("", "Credenciales inválidas");
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }
}
