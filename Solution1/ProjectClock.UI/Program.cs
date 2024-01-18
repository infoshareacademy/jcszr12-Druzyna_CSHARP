
using ProjectClock.BusinessLogic.Models;
using ProjectClock.BusinessLogic.Services;
using ProjectClock.UI.Menu;

namespace ProjectClock.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunMenu();
        }

        private static void RunMenu()
        {

            MainMenu menu = new MainMenu();

            menu.RunMainMenu();

            switch (menu.SelectedIndex)
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

                    ExitMenu exitMenu = new ExitMenu();
                    exitMenu.Run();
                    break;
            }
        }
    }
}
