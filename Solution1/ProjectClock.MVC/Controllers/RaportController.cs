using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjectClock.MVC.Controllers
{
    public class RaportController : Controller
    {

        [Authorize(Roles = "User")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
