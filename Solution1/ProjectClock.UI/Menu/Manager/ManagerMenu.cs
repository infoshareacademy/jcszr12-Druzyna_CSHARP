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
        private ManagerServicesProvider _managerServicesProvider = new ManagerServicesProvider();
        private Project _project;
        public string[] MainManagerMenuOptions { private set; get; } = new string[5] { "Create new project", "Remove project", "Display all projects", "Start work", "Exit" };

        internal User Run()
        {
            WriteLine("\nInsert your name:");

            string name = ReadLine();

            WriteLine("\nInsert your surname:");

            string surname = ReadLine();

            User manager = new User();

            manager.Name = name;
            manager.Surname = surname;
            manager.UserPosition = Position.Manager;

            RunSubManagerMenu();

            return manager;

        }

        private void RunSubManagerMenu()
        {
            bool wantExit = false;
            bool wantClear = true;
            bool projectCreated = false;

            do
            {                

                string prompt = "  "; 
                
                SelectedIndex = _menuServices.MoveableMenu(prompt, MainManagerMenuOptions, wantClear, MainMenu.Intro());                       
                              
                switch (SelectedIndex)
                {
                    case 0:

                        
                         _managerServicesProvider.CreateNewProject(out Project project);                                              

                        Console.WriteLine("\nPress any key to continue...");

                        Console.ReadKey(true); 

                        

                        break;

                    case 1:


                        break;

                    case 2:

                        _managerServicesProvider.ShowAllProjects();

                        Console.WriteLine("\nPress any key to continue...");

                        Console.ReadKey(true);           

                        break;

                    case 3:


                        break;

                    case 4:

                        ExitMenu exitMenu = new ExitMenu();
                        exitMenu.Run();
                        break;
                }

            } while (!wantExit);
            
           



        }

      

       
    }
}
