using ProjectClock.BusinessLogic.Models;
using ProjectClock.BusinessLogic.Services;
using ProjectClock.BusinessLogic.Services.ProjectServices;
using ProjectClock.UI.Menu.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
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

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nProject {projectName} has been created.");
            Console.ResetColor();

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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You entered either id that doesn't exit");
                Console.ResetColor();
            }

        }

        internal static void ModifyProject()
        {
            int oldId;
            int newId;

            string oldIdQuestion = "Insert id of a project you want to modify:";

            Console.WriteLine();
            Console.WriteLine(oldIdQuestion);

            while (!(int.TryParse(Console.ReadLine(), out oldId) && General.IdVerificator(oldId)))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You entered either non-integer or id that doesn't exist. Press Escape to exit or any other key to conitue.");
                Console.ResetColor();

                ExitMenu.ExitByPressingEscToManagerMenu();

                Console.WriteLine();
                Console.WriteLine(oldIdQuestion);
            }


            string newIdQuestion = $"Insert new id in place of old one {oldId}:";

            Console.WriteLine();
            Console.WriteLine(newIdQuestion);

            while (!(int.TryParse(Console.ReadLine(), out newId)))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You entered non-integer id. Press Escape to exit or any other key to conitue.");
                Console.ResetColor();

                ExitMenu.ExitByPressingEscToManagerMenu();

                Console.WriteLine();
                Console.WriteLine(newIdQuestion);
            }

            string newNameQuestion = "\nInsert new project name:";
            Console.WriteLine(newNameQuestion);
            string? newProjectName = Console.ReadLine();

            if (ProjectEditor.ModifyProject(oldId, newId, newProjectName))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Project id and/or name has been changed.");
                Console.ResetColor();
            }           
        }
    }
}
