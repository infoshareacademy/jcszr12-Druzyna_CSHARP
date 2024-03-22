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


        public async Task<IActionResult> Manage()
        {
            ManageOrganizationDto model = new ManageOrganizationDto();

            var organizations = await _organizationServices.GetAll();

            model.Organizations = organizations;
            //model.Projects = new List<Project>();
            //model.Users = new List<User>();

            return View("Manage", model);


        }

        [HttpPost]
        public async Task<IActionResult> Choose(int id)
        {

            ManageOrganizationDto model = new ManageOrganizationDto();

            var organizations = await _organizationServices.GetAll();

            model.Organizations = organizations;
            var projects = model.Organizations.FirstOrDefault(o => o.Id == id).Projects.ToList();
            model.Projects = model.Organizations.First(o => o.Id == id).Projects.ToList();
            var user = model.Organizations.First(o => o.Id == id).OrganizationUsers.Select(ou => ou.User).ToList();
            model.Users = model.Organizations.First(o => o.Id == id).OrganizationUsers.Select(ou => ou.User).ToList();



            //model.Users = model.Organizations.First(o => o.Id == id).OrganizationUsers
            //    .Where(o => o.Organization.Id == id).Select(u => u.User).ToList();

            return View("Manage", model);

                
        }

      

    }
}
