
using Newtonsoft.Json;
using ProjectClock.BusinessLogic.Models;
using ProjectClock.BusinessLogic.Services;
using ProjectClock.BusinessLogic.Services.UserServices;
using ProjectClock.UI.Menu;
using ProjectClock.BusinessLogic.Services.WorkingTimeRecorder;


namespace ProjectClock.UI
{   
    internal class Program
    {
        static void Main(string[] args)
        {

            MainMenu.RunMenu();
            //WorkingTimeRecorder.ClearDatabase();
        }

    }
}
