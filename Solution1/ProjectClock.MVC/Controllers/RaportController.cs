using Microsoft.AspNetCore.Mvc;

namespace ProjectClock.MVC.Controllers
{
    public class RaportController : Controller
    {
        public IActionResult Generate()
        {
            return View();
        }
    }
}
