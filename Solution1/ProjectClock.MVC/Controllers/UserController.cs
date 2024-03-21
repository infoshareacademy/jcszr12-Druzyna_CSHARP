using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectClock.BusinessLogic.Dtos.AccountDtos;
using ProjectClock.BusinessLogic.Services;
using ProjectClock.Database;

namespace ProjectClock.MVC.Controllers
{
    public class UserController : Controller
    {
        private ProjectClockDbContext _projectClockDbContext;
        private IUserServices _userServices;


        public UserController(ProjectClockDbContext projectClockDbContext, IUserServices userServices )
        {
            _userServices = userServices;
        }
        // GET: UserController
        public ActionResult Index()
        {
            var users = _userServices.GetAll();
            return View(users);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> GetUsersFromOrganization(int organizationId)
        {
            var users = await _userServices.GetAllFromOrganization(organizationId);
            return View(users);
        }
    }
}
