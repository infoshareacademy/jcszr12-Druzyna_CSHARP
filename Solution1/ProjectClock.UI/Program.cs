
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

            //WorkingTimeRecorder.WriteToDatabaseSimulationData();

            //WorkingTimeRecorder.StartWork(1,2);

            //WorkingTimeRecorder.StopWork(1,2);

            //WorkingTimeRecorder.ViewProjectsInProgress();

            //WorkingTimeRecorder.ViewClosedProjects();

            //WorkingTimeRecorder.TotalTimeForProjectID(3);

            //WorkingTimeRecorder.TotalTimeForUserID(4);

            //WorkingTimeRecorder.ClearDatabase();

            //////////////////////////////////////////////////////

            // MainMenu.RunMenu();

        }

    }
}
