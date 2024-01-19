
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
       
        }
    }
}
