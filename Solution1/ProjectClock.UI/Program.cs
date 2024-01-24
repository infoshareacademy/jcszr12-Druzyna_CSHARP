
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
            
            //////////////////////////////////////////////////////
            //              Demo WorkingTimeRecorder            //
            //////////////////////////////////////////////////////
            
            // WorkingTimeRecorder.WriteToDatabaseSimulationData();

            WorkingTimeRecorder.StartWork(4, 3);

            //WorkingTimeRecorder.StopWork(4, 3);

            //WorkingTimeRecorder.ViewProjectsInProgress();

            //WorkingTimeRecorder.ViewClosedProjects();

            //WorkingTimeRecorder.TotalTimeForProjectID(3);

            //WorkingTimeRecorder.TotalTimeForUserID(5);

            //WorkingTimeRecorder.ClearDatabase();


            //MainMenu.RunMenu();
            
        }         

    }
}
