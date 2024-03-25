using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectClock.BusinessLogic.Dtos.Organization;
using ProjectClock.BusinessLogic.Dtos.OrganizationDto;
using ProjectClock.BusinessLogic.Services;
using ProjectClock.Database;
using ProjectClock.Database.Entities;

namespace ProjectClock.MVC.Controllers
{
    public class OrganizationController : Controller
    {
        private IOrganizationServices _organizationServices;
        private IUserServices _userServices;
        private IMapper _mapper;

        public OrganizationController(IOrganizationServices organizationServices, IUserServices userServices, IMapper mapper)
        {
            _mapper = mapper;
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

        public ActionResult Create()
        {
            return View();
        }

        // POST: OrganizationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrganizationDto organizationDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                
                bool created = await _organizationServices.Create(organizationDto);

                if (created)
                {
                    TempData["SuccessMessage"] = "Organization created successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = "This organization already exists.";
                }

                return RedirectToAction(nameof(Create));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error occurred while deleting organization: {ex.Message}";
                return View();
            }
        }




        // GET: OrganizationController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _organizationServices.GetById(id);
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
        public async Task<IActionResult> Delete()
        {
            DeleteOrganizationDto model = new();

            var organizations = await _organizationServices.GetAll();
            model.Organizations = organizations;

            return View("Delete", model);
        }

        // POST: OrganizationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int organizationId)
        {
            try
            {
                bool deleted = await _organizationServices.Delete(organizationId);

                if (deleted)
                {
                    TempData["SuccessMessage"] = "Organization deleted successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = "This organization doesn't exists.";
                }

                return RedirectToAction(nameof(Delete));
            }
            catch
            {
                return View();
            }
        }


        public async Task<IActionResult> Manage()
        {
            ManageOrganizationDto model = new ManageOrganizationDto();

            var organizations = await _organizationServices.GetAll();

            model.Organizations = organizations;

            return View("Manage", model);

        }

        [HttpPost]
        public async Task<IActionResult> Choose(int organizationId, int userId)
        {
            ManageOrganizationDto model = new ManageOrganizationDto();

            var organizations = await _organizationServices.GetAll();
            var organization = organizations.FirstOrDefault(o => o.Id == organizationId);
            
            if (organization.OrganizationUsers.Count != 0)
            {
                var users = organization.OrganizationUsers.Select(ou => ou.User).ToList();
                var user = users.FirstOrDefault(u => u.Id == userId);

                model.Users = users;
                model.User = user;
            }
            else
            {
                TempData["NoUsersMessage"] = "This organization hasn't got users yet.";
                model.Users = new List<User>();
                model.User = null;
            }

            model.OrganizationId = organizationId;
            model.Organizations = organizations;

            return View("Manage", model);
        }
    }
}
