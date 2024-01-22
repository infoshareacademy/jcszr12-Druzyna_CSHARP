using ProjectClock.BusinessLogic.Models;
using ProjectClock.BusinessLogic.Services;
using ProjectClock.BusinessLogic.Services.ProjectServices;
using ProjectClock.UI.Menu.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.UI.Menu.Manager
{
    internal static class ManagerServicesProvider
    {

        internal void CreateNewProject()
        {

            Console.WriteLine("\nInsert the name of new project:");

            string projectName = Console.ReadLine();

            ProjectCreator.CreateProject(projectName);

            Console.WriteLine($"\nProject {projectName} has been created.");

        }

        internal void ShowAllProjects()
        {
            var projects = ProjectGetter.GetProjectList();

            Console.WriteLine("\nList of projects:");

            foreach (var project in projects)
            {
                Console.WriteLine($" Project Id: {project.Id, -5}    Name: {project.Name, -30}");
            }
        }

    }
}
