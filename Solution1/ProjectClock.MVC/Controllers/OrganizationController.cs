using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectClock.BusinessLogic.Services;
using ProjectClock.Database.Entities;

namespace ProjectClock.MVC.Controllers
{
    public class OrganizationController : Controller
    {
        private OrganizationServices _organizationServices;

        public OrganizationController(OrganizationServices organizationServices)
        {
            _organizationServices = organizationServices;
        }

        // GET: OrganizationController
        public async Task<IActionResult> Index()
        {
            var list = await _organizationServices.GetAll();
            return View(list);
        }

      
        // GET: OrganizationController/Details/5
        public ActionResult Details(int id)
        {
            var organization = _organizationServices.GetById(id);
            return View(organization);
        }

        // GET: OrganizationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrganizationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Organization model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                _organizationServices.Create(model);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrganizationController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _organizationServices.GetById(id);
            return View(model);
        }

        // POST: OrganizationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Organization model)
        {

            try
            {
                _organizationServices.Update(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrganizationController/Delete/5
        public ActionResult Delete(int id)
        {
            var organization = _organizationServices.GetById(id);
            return View(organization);
        }

        // POST: OrganizationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _organizationServices.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
