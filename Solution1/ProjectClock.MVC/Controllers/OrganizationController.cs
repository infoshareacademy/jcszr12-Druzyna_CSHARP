using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectClock.BusinessLogic.Dtos.Organization;
using ProjectClock.BusinessLogic.Dtos.OrganizationDto;
using ProjectClock.BusinessLogic.Services.AccountServices;
using ProjectClock.BusinessLogic.Services.OrganizationServices;
using ProjectClock.BusinessLogic.Services.OrganizationUserServices;
using ProjectClock.BusinessLogic.Services.UserServices;
using ProjectClock.Database;
using ProjectClock.Database.Entities;
using ProjectClock.MVC.Extensions;

namespace ProjectClock.MVC.Controllers
{
    public class OrganizationController : Controller
    {
        private IOrganizationServices _organizationServices;
        private IUserServices _userServices;
        private IAccountServices _accountService;
        private IOrganizationUserServices _organizationUserServices;
        private IMapper _mapper;

        public OrganizationController(IOrganizationServices organizationServices, 
            IUserServices userServices, 
            IAccountServices accountService, 
            IOrganizationUserServices organizationUserServices,
            IMapper mapper)
        {
            _mapper = mapper;
            _userServices = userServices;
            _organizationServices = organizationServices;
            _accountService = accountService;
            _organizationUserServices = organizationUserServices;
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
                    TempData["ErrorMessage"] = "You didn't enter name of organization.";
                    return View();
                }


                HttpContext.User.Claims.TryGetAuthenticatedUserId(out var accountId);
                organizationDto.UserId = await _accountService.GetUserIdFromAccountId(accountId);

                if (_organizationUserServices.IsUserAnOwner(organizationDto.UserId))
                {
                    TempData["ErrorMessage"] = "You are already an owner of organization. You can only be owner of one organization.";
                }
                else
                {
                    bool created = await _organizationServices.Create(organizationDto);

                    if (created)
                    {
                        TempData["SuccessMessage"] = "Organization created successfully.";

                    }
                    else
                    {
                        TempData["ErrorMessage"] = "This organization already exists.";
                    }
                }

                return RedirectToAction(nameof(Create));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error occurred while creating organization: {ex.Message}";
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

            HttpContext.User.Claims.TryGetAuthenticatedUserId(out var accountId);
            int userId = await _accountService.GetUserIdFromAccountId(accountId);

            var userOrganizations = await _organizationUserServices.GetUserOrganizations(userId);

            var organizationDtoList = userOrganizations.Select(x => new OrganizationDto()
            {
                OrganizationId = x.Id,
                OrganizationName = x.Name
            }).ToList();
           
            model.Organizations = organizationDtoList;

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

            if (organizationId == 0)
            {
                TempData["NoOrganizationChoosed"] = "You didn't choose organization.";
                var organizations = await _organizationServices.GetAll();
                model.Organizations = organizations;
                return View("Manage", model);

            }
            else
            {
                model = await GetManageOrganizationDto(organizationId, userId);
                return View("Manage", model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> InviteUser(int organizationId, string email, int userId)
        {
            var user = await _userServices.GetByEmail(email);
            var organization = await _organizationServices.GetById(organizationId);

            ManageOrganizationDto model = new ManageOrganizationDto();

            if (user is null)
            {
                model = await GetManageOrganizationDto(organizationId, userId);
                TempData["UserAddedFailedMessage"] =
                    $"User with email: {email} hasn't been added to the organization {organizationId}. User is not registered in system.";
            }
            else
            {
                userId = user.Id;

                bool invited = await _organizationServices.AddUser(organizationId, userId);

                if (invited)
                {
                    TempData["UserAddedMessage"] = $"User with email: {email} has been added to the organization {organization.Name}.";
                }
                else
                {
                    TempData["UserAddedFailedMessage"] =
                        $"User with email: {email} hasn't been added to organization with {organization.Name}.";
                }

                model = await GetManageOrganizationDto(organizationId, userId);
            }

            return View("Manage", model);
        }

        private async Task<ManageOrganizationDto> GetManageOrganizationDto(int organizationId, int userId)
        {
            ManageOrganizationDto model = new ManageOrganizationDto();

            var organizations = await _organizationServices.GetAll();
            var organization = organizations.FirstOrDefault(o => o.Id == organizationId);
            var allUsers = await _userServices.GetAll();

            if (organization?.OrganizationUsers?.Count > 0)
            {
                var users = organization.OrganizationUsers.Select(ou => ou.User).ToList();
                var user = users.FirstOrDefault(u => u.Id == userId);

                model.OrganizationUsers = users;
                model.User = user;
            }
            else
            {
                TempData["NoUsersMessage"] = "This organization hasn't got users yet.";
                model.OrganizationUsers = new List<User>();
                model.User = null;
            }

            model.OrganizationId = organizationId;
            model.Organizations = organizations;
            model.AllUsers = allUsers;
            model.Organization = organization;
            model.SelectedOrganizationId = organizationId;

            return model;
        }

    }
}
