using ProjectClock.BusinessLogic.Models;
using ProjectClock.BusinessLogic.Services;
using ProjectClock.BusinessLogic.Services.EntryTimeServices;
using ProjectClock.BusinessLogic.Services.ProjectServices;
using ProjectClock.BusinessLogic.Services.WorkingTimeRecorder;
using ProjectClock.UI.Menu.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.UI.Menu.Manager
{
    internal class ManagerServicesProvider
    {
        public static int SelectedIndex { private set; get; }
        public static int _idOfChoosenProject { private set; get; }
        public static Project _selectedProject { private set; get; }
        public static List<Project> _projects { get; private set; } = ProjectGetter.GetProjectList();

        internal static void CreateNewProject()
        {

            Console.WriteLine("\nInsert the name of new project:");

            string projectName = Console.ReadLine();

            ProjectCreator.CreateProject(projectName);

            _projects = ProjectGetter.GetProjectList();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nProject {projectName} has been created.");
            Console.ResetColor();

        }

        internal static void ShowAllProjects()
        {

            Console.WriteLine("\nList of projects:");

            foreach (var project in _projects)
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
                Console.ResetColor();            }

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

            while (!int.TryParse(Console.ReadLine(), out newId) || (General.IdVerificator(newId)))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You entered either non-integer id or id that exist. Press Escape to exit or any other key to conitue.");
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

        internal static void StopWorking()
        {
            //do implementacji

            //Console.WriteLine($"\nYor work stopped on \"{_selectedProject.Name}\".");

        }

        internal static void StartWorking()
        {
            string prompt = String.Format("Choose project you want to work on:\n");

            var projectNames = _projects.Select(p => p.Name).ToList();

            SelectedIndex = MenuServices.MoveableMenu(prompt, projectNames, MainMenu.Intro());

            _idOfChoosenProject = _projects.ElementAt(SelectedIndex).Id;
            _selectedProject = _projects.ElementAt(SelectedIndex);
            int userId = MainMenu.User.Id;


            if (WorkingTimeRecorder.StartWork(userId, _idOfChoosenProject)) 
            {
                Console.WriteLine($"\nYour work began at \"{_selectedProject.Name}\".");
            }
            else
            {
                Console.WriteLine($"\nProject with ID {_idOfChoosenProject} for user with ID: {userId} is in progres. Choose another one. \n");
            }

        }
    }
}
