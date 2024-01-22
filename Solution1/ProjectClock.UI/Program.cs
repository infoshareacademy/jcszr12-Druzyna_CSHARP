
using Newtonsoft.Json;
using ProjectClock.BusinessLogic.Models;
using ProjectClock.BusinessLogic.Services;
using ProjectClock.BusinessLogic.Services.UserServices;
using ProjectClock.UI.Menu;


namespace ProjectClock.UI
{   
    internal class Program
    {
        static void Main(string[] args)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.RunMainMenu();
        }         

    }
}
