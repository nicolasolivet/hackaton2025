using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ctrlcctrlv.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "/")
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(string username, string password, string returnUrl = "/")
        {
            // Usuario de prueba (modo placeholder)
            if (username == "test" && password == "test")
            {
                HttpContext.Session.SetString("UsuarioLogueado", username);
                return RedirectToAction("MenuTutor", "Tutores");
            }

            ModelState.AddModelError("", "Usuario o contraseña incorrectos (usar: test / test).");
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("", "Completá todos los campos.");
                return View();
            }

            // No guardamos nada aún. Solo simulamos que se creó.
            TempData["RegisterMessage"] = "Cuenta creada correctamente. Ahora iniciá sesión.";

            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
