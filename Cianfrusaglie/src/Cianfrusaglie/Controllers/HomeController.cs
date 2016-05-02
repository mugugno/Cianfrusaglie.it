using Microsoft.AspNet.Mvc;

namespace Cianfrusaglie.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            ViewData["Message"] = "Pagina d'errore";
            return View();
        }
    }
}
