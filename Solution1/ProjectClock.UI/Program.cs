
using Newtonsoft.Json;
using ProjectClock.BusinessLogic.Models;
using ProjectClock.BusinessLogic.Services;
using ProjectClock.BusinessLogic.Services.UserServices;
using ProjectClock.UI.Menu;
using ProjectClock.BusinessLogic.Services.WorkingTimeRecorder;
using System.Dynamic;
using ProjectClock.BusinessLogic.Services.Statistics;


namespace ProjectClock.UI
{   
    internal class Program
    {
        static void Main(string[] args)
        {
            MainMenu.RunMenu();
        }

    }
}
