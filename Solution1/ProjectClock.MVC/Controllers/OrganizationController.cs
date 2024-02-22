using Microsoft.AspNetCore.Mvc;

namespace ProjectClock.MVC.Controllers
{
    public class OrganizationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
