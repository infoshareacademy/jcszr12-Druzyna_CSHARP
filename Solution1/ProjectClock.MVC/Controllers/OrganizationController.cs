using Microsoft.AspNetCore.Mvc;
using ProjectClock.BusinessLogic.Dtos.OrganizationDto;
using ProjectClock.BusinessLogic.Services;
using ProjectClock.Database.Entities;

namespace ProjectClock.MVC.Controllers
{
    public class OrganizationController : Controller
    {
        private IOrganizationServices _organizationServices;
        private IUserServices _userServices;

        public OrganizationController(IOrganizationServices organizationServices, IUserServices userServices)
        {
            _userServices = userServices;
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
        public async Task<IActionResult> Create(OrganizationDto organizationDto)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return View(model);
                //}

                await _organizationServices.Create(organizationDto);

                return RedirectToAction(nameof(Create));
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


        public async Task<IActionResult> Manage(OrganizationDto organizationDto)
        {
            var list = await _organizationServices.GetAll();
            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> Choose(int id)
        {
            // Pobierz listę użytkowników dla danej organizacji na podstawie identyfikatora (id)
            var users = await _userServices.GetAllFromOrganization(id);

            // Przekazanie listy użytkowników jako modelu do częściowego widoku
            return RedirectToAction("GetUsersFromOrganization", "User", new { users = users });
        }

    }
}
