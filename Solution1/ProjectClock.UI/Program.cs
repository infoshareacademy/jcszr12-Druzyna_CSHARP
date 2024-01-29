﻿
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
            //MainMenu.RunMenu();
            //


            //Console.WriteLine($"\n\n  * Total time for the projects about ID {1} is: {Statistics.TotalTimeForProjectID(1).Days}D {Statistics.TotalTimeForProjectID(1).Hours}H {Statistics.TotalTimeForProjectID(1).Minutes}m {Statistics.TotalTimeForProjectID(1).Seconds}s");

           // Console.WriteLine($"\n\n  * Total time on the projects for user about ID {1} is: {Statistics.TotalTimeForUserID(1).Days}D {Statistics.TotalTimeForUserID(1).Hours}H {Statistics.TotalTimeForUserID(1).Minutes}m {Statistics.TotalTimeForUserID(1).Seconds}s");

             Console.WriteLine($"\n\n  * Total time for all projects worked on is: {Statistics.TotalTimeForAllProjectsWorkedOn().Days}D {Statistics.TotalTimeForAllProjectsWorkedOn().Hours}H {Statistics.TotalTimeForAllProjectsWorkedOn().Minutes}m {Statistics.TotalTimeForAllProjectsWorkedOn().Seconds}s");







           // Statistics.DemoTotalTimeForAllProjectsWorkedOn();

            //Statistics.DemoTotalTimeForProjectID(2);
        }

    }
}
