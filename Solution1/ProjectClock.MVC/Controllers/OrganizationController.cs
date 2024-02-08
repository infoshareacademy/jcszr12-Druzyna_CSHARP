using Microsoft.AspNetCore.Mvc;

namespace ProjectClock.MVC.Controllers
{
    public class OrganizationController : Controller
    {
        public IActionResult Manage()
        {
            return View();
        }
    }
}
