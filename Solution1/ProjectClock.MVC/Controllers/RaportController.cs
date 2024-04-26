using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectClock.BusinessLogic.Dtos.OrganizationDto;
using ProjectClock.BusinessLogic.Services.WorkingTimeServices;

using ProjectClock.BusinessLogic.Services.UserServices;
using ProjectClock.BusinessLogic.Services.ProjectServices;

using ProjectClock.BusinessLogic.Services;
using ProjectClock.BusinessLogic.Services.RaportServices;

using ProjectClock.BusinessLogic.Dtos.WorkingTime.WorkingTimeDtos;
using ProjectClock.BusinessLogic.Dtos.Raport;
using ProjectClock.Database.Entities;
using System.Collections.Generic;
using ProjectClock.Database;
using Microsoft.EntityFrameworkCore;
using ProjectClock.BusinessLogic.Dtos.Organization;
using ProjectClock.MVC.Extensions;
using Humanizer;


namespace ProjectClock.MVC.Controllers
{
    public class RaportController : Controller
    {

        private readonly IUserServices _userService;
        private readonly IProjectServices _projectService;
        private readonly IWorkingTimeServices _workingTimeServices;
        private readonly IRaportServices _raportServices;
        private readonly ILogger<RaportController> _logger;

        private readonly ProjectClockDbContext _dbContext;



        public RaportController(IUserServices userService, IProjectServices projectService, IWorkingTimeServices workingTimeServices, ILogger<RaportController> logger, IRaportServices raportService, ProjectClockDbContext dbContext)
        {
            _userService = userService;
            _projectService = projectService;
            _workingTimeServices = workingTimeServices;
            _logger = logger;
            _raportServices = raportService;
            _dbContext = dbContext;
        }



        



        //======================================================= User ==============================================================


        [Authorize(Roles = "User")]
        public async Task<IActionResult> User()
        {

            Model dto = new Model();


            dto.ListUsersForRaports = await _userService.GetAll();


            int zm = dto.ListUsersForRaports.FirstOrDefault(a => a.Id > 0).Id;


            dto = await _raportServices.GetProjectNameAndTimeForUser(zm);


            TempData["Id"] = zm.ToString();


            return View(dto);

        }


        // POST: OrganizationController/Create
        [HttpPost]
        //	[ValidateAntiForgeryToken]
        public async Task<IActionResult> Wiii(int userId)
        {

            try
            {
                var dto = await _raportServices.GetProjectNameAndTimeForUser(userId);

                TempData["Id"] = userId.ToString();

                return View("User", dto);

            }
            catch (Exception ex)
            {

                return View();
            }

        }


        [Authorize(Roles = "User")]
        //[HttpGet]
        public async Task<IActionResult> ChartOfUser()
        {

            int zm = int.Parse(TempData["Id"].ToString());

            var dto = await _raportServices.GetDataForChart(zm);

            return View(dto);

        }



		//======================================================= Project ==============================================================



		[Authorize(Roles = "User")]
		public async Task<IActionResult> Project()
		{


			Model dto = new Model();


			dto.ListProjectsForRaports = await _raportServices.GetAllProjects();


			//int zm  =     dto.ListProjectsForRaports.FirstOrDefault(a => a.Id > 0).Id;


			//dto = await _raportServices.GetUserNameAndTimeForProject(zm);


			//TempData["Id_2"] = zm_2.ToString();


			return View(dto);
			
		}


		// POST: OrganizationController/Create
		[HttpPost]
		//	[ValidateAntiForgeryToken]
		public async Task<IActionResult> Wiii_2(int userId)
		{

			try
			{
				var dto = await _raportServices.GetProjectNameAndTimeForUser(userId);

				TempData["Id"] = userId.ToString();

				return View("User", dto);

			}
			catch (Exception ex)
			{

				return View();
			}

		}






		//======================================================= Organization ==============================================================



		[Authorize(Roles = "User")]
		public IActionResult Organization()
		{
			return View();
		}




	}
}
