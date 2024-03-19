using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectClock.BusinessLogic.Dtos.WorkingTime.WorkingTimeDtos;
using ProjectClock.BusinessLogic.Services;
using ProjectClock.BusinessLogic.Services.WorkingTimeServices;
using ProjectClock.MVC.Extensions;
using ProjectClock.MVC.Models;
using System.Diagnostics;


namespace ProjectClock.MVC.Controllers
{
    public class HomeController : Controller
    {       
        private readonly IProjectServices _projectService;
        private readonly IAccountService _accountService;
        private readonly IWorkingTimeServices _workingTimeServices;
        

        public HomeController(IProjectServices serviceProject,
            IWorkingTimeServices workingTimeServices, 
            IAccountService accountService)
        {
            _projectService = serviceProject;            
            _accountService = accountService;
            _workingTimeServices = workingTimeServices;
        }

        [Authorize(Roles = "User")]
        public async Task <IActionResult> Index()
        {
            var dto = new StartWorkingTimeDto();
            dto.Projects = await _projectService.GetAll(); //TODO: Write method giving me only projects from user organization
            return View(dto);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Index(StartWorkingTimeDto dto)
        {
            if (!HttpContext.User.Claims.TryGetAuthenticatedUserId(out var accountId))
            {
                return RedirectToAction("Index", "Home");
            }

            dto.UserId = await _accountService.GetUserIdFromAccountId(accountId);

            dto.Projects = await _projectService.GetAll();

            await _workingTimeServices.Create(dto);

            return View(dto);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
