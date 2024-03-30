using Microsoft.AspNetCore.Mvc;
using ProjectClock.BusinessLogic.Dtos.Project.ProjectDtos;
using ProjectClock.BusinessLogic.Services;
using ProjectClock.MVC.Extensions;

namespace ProjectClock.MVC.Controllers
{
    public class NewProjectController : Controller
    {
        private readonly IProjectServices _serviceProject;
        private readonly IOrganizationServices _serviceOrganization;
        private readonly IAccountService _accountService;

        public NewProjectController(IProjectServices serviceProject, 
            IOrganizationServices serviceOrganization,
            IAccountService accountService)
        {
            _serviceProject = serviceProject;
            _serviceOrganization = serviceOrganization;
            _accountService = accountService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _serviceProject.GetAll();

            return View(list);
        }

        
    }
}
