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

        internal static void CreateNewProject()
        {

            Console.WriteLine("\nInsert the name of new project:");

            string projectName = Console.ReadLine();

            ProjectCreator.CreateProject(projectName);

            Console.WriteLine($"\nProject {projectName} has been created.");

        }

        internal static void ShowAllProjects()
        {
            var projects = ProjectGetter.GetProjectList();

            Console.WriteLine("\nList of projects:");

            foreach (var project in projects)
            {
                Console.WriteLine($" Project Id: {project.Id,-5}    Name: {project.Name,-30}");
            }
        }

        internal static void RemoveProject()
        {
            Console.WriteLine("\nInsert Id of a project you want to remove:");                       

            if (int.TryParse(Console.ReadLine(), out int id) && ProjectRemover.RemoveProject(id))
            {               
               Console.WriteLine($"Project with Id {id} has been removed.");               
            }
            else
            {
                Console.WriteLine("You entered either id that doesn't exit");
            }

        }

    }
}
