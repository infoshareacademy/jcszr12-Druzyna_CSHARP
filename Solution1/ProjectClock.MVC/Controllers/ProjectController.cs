
using Microsoft.AspNetCore.Mvc;
using ProjectClock.BusinessLogic.Services.ProjectServices;
using System.Numerics;

namespace ProjectClock.MVC.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            ProjectCreator.CreateProject(name);
            return RedirectToAction(nameof(Create));
        }
        public IActionResult Delete()
        {
            var list = ProjectGetter.GetProjectList();
            return View(list);
        }
        [HttpPost]
        //TO DO refactor remove method we need insert project name not id
        public async Task<IActionResult> Delete(string name)
        {
            var list = ProjectGetter.GetProjectList();
            int id = 0;
            foreach(var project in list)
            {
                if(project.Name == name)
                {
                    id = project.Id;
                }
            }
            ProjectRemover.RemoveProject(id);
            return RedirectToAction(nameof(Delete));
        }
    }
}
