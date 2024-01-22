using ProjectClock.BusinessLogic.Models;
using ProjectClock.BusinessLogic.Services;
using ProjectClock.UI.Menu.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ProjectClock.UI.Menu.Manager
{
    internal class ManagerMenu
    {
        public int SelectedIndex { private set; get; }
        private MenuServices _menuServices = new MenuServices();
        private Project _project;
        public string[] MainManagerMenuOptions { private set; get; } = new string[5] {
            "Create new project",
            "Remove project",
            "Display all projects",
            "Modify existing project",
            "Exit"
        };

        //internal User Run()
        //{
        //    WriteLine("\nInsert your name:");

        //    string name = ReadLine();

        //    WriteLine("\nInsert your surname:");

        //    string surname = ReadLine();

        //    User manager = new User();

        //    manager.Name = name;
        //    manager.Surname = surname;
        //    manager.UserPosition = Position.Manager;

        //    RunSubManagerMenu();

        //    return manager;

        //}

        internal void RunManagerMenu()
        {
            bool wantExit = false;
            bool wantClear = true;
            bool projectCreated = false;

            do
            {

                string prompt = "  ";

                SelectedIndex = MenuServices.MoveableMenu(prompt, MainManagerMenuOptions, wantClear, MainMenu.Intro());

                switch (SelectedIndex)
                {
                    case 0:

                        ManagerServicesProvider.CreateNewProject();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey(true);
                        break;

                    case 1:

                        ManagerServicesProvider.RemoveProject();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey(true);
                        break;                        

                    case 2:

                        ManagerServicesProvider.ShowAllProjects();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey(true);
                        break;

                    case 3:

                        ManagerServicesProvider.ModifyProject();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey(true);
                        break;

                    case 4:

                        ExitMenu.ExitFromProgramUsingAnyKey();                     
                        break;
                }

            } while (!wantExit);

        }

    }
}
