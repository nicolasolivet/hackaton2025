using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ctrlcctrlv.Controllers
{
    [Authorize] // exige autenticación para acceder al Index
    public class HomeController : Controller
    {
        // GET: /Home/Index (o /)
        public IActionResult Index()
        {
            // Si querés redirigir a MenuTutor cuando ingrese el usuario, podes hacerlo aquí:
            // return RedirectToAction("MenuTutor", "Tutores");

            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
