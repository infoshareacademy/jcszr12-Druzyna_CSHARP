using ProjectClock.BusinessLogic.Models;
using ProjectClock.BusinessLogic.Services;
using ProjectClock.UI.Menu.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.UI.Menu.Manager
{
    internal class ManagerServicesProvider
    {
        public Project Project { get; private set; }
        
        
        public void CreateNewProject(out Project project)
        {                      
            
            Console.WriteLine("Insert the name of new project:");

            string projectName = Console.ReadLine();

            project = new Project();

            project.Name = projectName;
            
        }

        private void ShowAllProjects()
        {
            MenuServices menuServices = new MenuServices();
            var projectNames = GetProjects();
            projectNames[projectNames.Length - 1] = "Exit";
            string prompt = "Choose your project: ";

            //MenuServices menuService = new MenuServices();

            //SelectedIndex = menuService.MoveableMenu(prompt, projectNames);
        }

        private string[] GetProjects()
        {

            var projects = ProjectServicesProjectGetter.GetProjectList();

            return projects.Select(x => x.Name).ToList().ToArray();

        }

    }
}
