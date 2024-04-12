using Microsoft.AspNetCore.Mvc;
using ProjectClock.BusinessLogic.Dtos.Project.ProjectDtos;
using ProjectClock.BusinessLogic.Services.AccountServices;
using ProjectClock.BusinessLogic.Services.OrganizationServices;
using ProjectClock.BusinessLogic.Services.ProjectServices;
using ProjectClock.MVC.Extensions;

namespace ProjectClock.MVC.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectServices _projectServices;
        private readonly IOrganizationServices _organizationServices;
        private readonly IAccountServices _accountServices;

        public ProjectController(IProjectServices serviceProject, 
            IOrganizationServices serviceOrganization,
            IAccountServices accountService)
        {
            _projectServices = serviceProject;
            _organizationServices = serviceOrganization;
            _accountServices = accountService;
        }

        public async Task<IActionResult> Index()
        {
            if (!HttpContext.User.Claims.TryGetAuthenticatedUserId(out var accountId))
            {
                return RedirectToAction("Index", "Home");
            }

            var userId = await _accountServices.GetUserIdFromAccountId(accountId);
            var dtos = await _projectServices.GetAllUserProjects(userId);

            return View(dtos);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectDto dto)
        {
            await _projectServices.Create(dto);

            return RedirectToAction(nameof(Index));
        }

        
    }
}
