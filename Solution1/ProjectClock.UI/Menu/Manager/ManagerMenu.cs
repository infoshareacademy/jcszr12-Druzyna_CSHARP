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
            MenuServices menuServices = new MenuServices();

            string prompt = "  ";

            MenuServices menuService = new MenuServices();

            SelectedIndex = menuService.MoveableMenu(prompt, MainManagerMenuOptions, MainMenu.Intro());

            switch (SelectedIndex)
            {
                case 0:

                    ManagerMenu managerMenu = new ManagerMenu();
                    managerMenu.Run();
                    break;

                case 1:

                    UserMenu userMenu = new UserMenu();
                    userMenu.Run();
                    break;

                case 2:


                    break;

                case 3:


                    break;

                case 4:

                    ExitMenu exitMenu = new ExitMenu();
                    exitMenu.Run();
                    break;
            }



        }

      

       
    }
}
