using Microsoft.AspNetCore.Mvc;
using ProjectClock.BusinessLogic.Services;
using ProjectClock.Database.Entities;

namespace ProjectClock.MVC.Controllers
{
    public class ProjectController : Controller
    {
        
        private readonly IProjectServices _serviceProject;
        

        public ProjectController(IProjectServices serviceProject)
        {           
            _serviceProject = serviceProject;
            
        }
        public IActionResult Index()
        {
            var list = _serviceProject.GetAll();
            return View(list);
        }
        [HttpPost]
        public async Task<IActionResult> Index([Bind("Name")]Project project)
        {
            await _serviceProject.Create(project);
            return RedirectToAction(nameof(Index));
        }

        //TO DO refactor remove method we need insert project name not id
        [Route("Project/{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            var list = await _serviceProject.GetAll();
            int id = 0;
            foreach (var project in list)
            {
                if (project.Name == name)
                {
                    id = project.Id;
                }
            }
            _serviceProject.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
