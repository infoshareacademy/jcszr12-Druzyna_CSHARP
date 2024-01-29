using Newtonsoft.Json;
using ProjectClock.BusinessLogic.Models.DataTimeRecorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Services.Statistics
{
    public class Statistics
    {


        public static TimeSpan TotalTimeForProjectId(int projectId)
        {
            var dataFromDatabase = GetDataStopTimeFromDatabase();
            var dataProjectId = dataFromDatabase.Where(p => p.ProjectID == projectId);
            List<TimeSpan> timeSpans = [];
            foreach (var result in dataProjectId)
            {
                TimeSpan timeSpan = result.TimeStop.Subtract(result.TimeStart);
                timeSpans.Add(timeSpan);
            }
            long timeSpanTics = timeSpans.Sum(p => p.Ticks);
            TimeSpan timeSpanRaw = new TimeSpan(timeSpanTics);
            return timeSpanRaw;
        }

        public static TimeSpan TotalTimeForUserId(int userId)
        {

            var dataFromDatabase = GetDataStopTimeFromDatabase();
            var dataUserId = dataFromDatabase.Where(p => p.UserID == userId);
            List<TimeSpan> timeSpans = [];
            foreach (var result in dataUserId)
            {
                TimeSpan timeSpan = result.TimeStop.Subtract(result.TimeStart);
                timeSpans.Add(timeSpan);
            }
            long timeSpanTics = timeSpans.Sum(p => p.Ticks);
            TimeSpan timeSpanRaw = new TimeSpan(timeSpanTics);
            return timeSpanRaw;
        }

        public static TimeSpan TotalTimeForAllProjectsWorkedOn()
        {
            var dataFromDatabase = GetDataStopTimeFromDatabase();
            var dataProjectId = dataFromDatabase;
            List<TimeSpan> timeSpans = [];
            
            foreach (var result in dataProjectId)
            {
                TimeSpan timeSpan = result.TimeStop.Subtract(result.TimeStart);             
                timeSpans.Add(timeSpan);
            }
            long timeSpanTics = timeSpans.Sum(p => p.Ticks);
            TimeSpan timeSpanRaw = new TimeSpan(timeSpanTics);
            return timeSpanRaw;
        }

        public static TimeSpan TotalTimeForAllUsersWhoWorked()
        {

            var dataFromDatabase = GetDataStopTimeFromDatabase();
            var dataProjectId = dataFromDatabase;
            List<TimeSpan> timeSpans = [];
            foreach (var result in dataProjectId)
            {
                TimeSpan timeSpan = result.TimeStop.Subtract(result.TimeStart);
                timeSpans.Add(timeSpan);
            }
            long timeSpanTics = timeSpans.Sum(p => p.Ticks);
            TimeSpan timeSpanRaw = new TimeSpan(timeSpanTics);
            return timeSpanRaw;
        }

        public static TimeSpan WorkTimeForUserIdAndProjectId(int userId, int projectId)
        {

            var dataFromDatabase = GetDataStopTimeFromDatabase();
            var dataUserId = dataFromDatabase.Where(p => p.UserID == userId).Where(p => p.ProjectID == projectId);
            List<TimeSpan> timeSpans = [];
            foreach (var result in dataUserId)
            {
                TimeSpan timeSpan = result.TimeStop.Subtract(result.TimeStart);
                timeSpans.Add(timeSpan);
            }
            long timeSpanTics = timeSpans.Sum(p => p.Ticks);
            TimeSpan timeSpanRaw = new TimeSpan(timeSpanTics);
            return timeSpanRaw;
        }





        private static List<StopWork> GetDataStopTimeFromDatabase()
        {
            var json = File.ReadAllText(GetDirectoryToFileFromDataFolder("recordsOfTimeStop.json"));
            List<StopWork> dataTimeRecords = JsonConvert.DeserializeObject<List<StopWork>>(json);
            return dataTimeRecords;
        }

        private static string GetDirectoryToFileFromDataFolder(string fileName)
        {
            var pathOrigin = Directory.GetCurrentDirectory();
            var index = pathOrigin.IndexOf("Solution1");
            var pathPart = pathOrigin.Substring(0, index);
            var path = $@"{pathPart}\Solution1\ProjectClock.BussinesLogic\Data\WorkingTimeRecorder\{fileName}";
            return path;
        }

       

        public static TimeSpan DemoTotalWorkTimeForProjectId(int projectId)
        {

            var dataFromDatabase = GetDataStopTimeFromDatabase();
            var dataProjectId = dataFromDatabase.Where(p => p.ProjectID == projectId);
            List<TimeSpan> timeSpans = [];
            Console.WriteLine($"\n\n * Data for projects about ID: {projectId} \n");
            Console.WriteLine("   * Individual times for projects that were worked on: \n");
            foreach (var result in dataProjectId)
            {
                TimeSpan timeSpan = result.TimeStop.Subtract(result.TimeStart);
                Console.WriteLine($"     * ID: {result.ProjectID} is {timeSpan}");
                timeSpans.Add(timeSpan);
            }
            long timeSpanTics = timeSpans.Sum(p => p.Ticks);
            TimeSpan timeSpanRaw = new TimeSpan(timeSpanTics);
            Console.WriteLine($"\n\n  * Total time for the projects about ID {projectId} is: {timeSpanRaw.Days}D {timeSpanRaw.Hours}H {timeSpanRaw.Minutes}m {timeSpanRaw.Seconds}s \n\n\n\n");

            return timeSpanRaw;
        }

        public static TimeSpan DemoTotalWorkTimeForAllProjectsWorkedOn()
        {

            var dataFromDatabase = GetDataStopTimeFromDatabase();
            var dataProjectId = dataFromDatabase;
            List<TimeSpan> timeSpans = [];
            Console.WriteLine($"\n\n  * Total time for all projects:  \n");
            Console.WriteLine("\n    * Individual times for projects that were worked on: \n");
            foreach (var result in dataProjectId)
            {
                TimeSpan timeSpan = result.TimeStop.Subtract(result.TimeStart);
                Console.WriteLine($"     * ID: {result.ProjectID} is {timeSpan}");
                timeSpans.Add(timeSpan);
            }
            long timeSpanTics = timeSpans.Sum(p => p.Ticks);
            TimeSpan timeSpanRaw = new TimeSpan(timeSpanTics);
            Console.WriteLine($"\n\n  * Total time for all projects worked on is: {timeSpanRaw.Days}D {timeSpanRaw.Hours}H {timeSpanRaw.Minutes}m {timeSpanRaw.Seconds}s \n\n\n\n");

            return timeSpanRaw;
        }

        public static TimeSpan DemoTotalWorkTimeForAllUsersWhoWorked()
        {

            var dataFromDatabase = GetDataStopTimeFromDatabase();
            var dataProjectId = dataFromDatabase;
            List<TimeSpan> timeSpans = [];
            Console.WriteLine($"\n\n  * Total time for all usars who worked:  \n");
            Console.WriteLine("\n    * Individual times for users who worked: \n");
            foreach (var result in dataProjectId)
            {
                TimeSpan timeSpan = result.TimeStop.Subtract(result.TimeStart);
                Console.WriteLine($"     * ID: {result.UserID} is {timeSpan}");
                timeSpans.Add(timeSpan);
            }
            long timeSpanTics = timeSpans.Sum(p => p.Ticks);
            TimeSpan timeSpanRaw = new TimeSpan(timeSpanTics);
            Console.WriteLine($"\n\n  * Total time for all users who worked is: {timeSpanRaw.Days}D {timeSpanRaw.Hours}H {timeSpanRaw.Minutes}m {timeSpanRaw.Seconds}s \n\n\n\n");

            return timeSpanRaw;
        }

        public static TimeSpan DemoTotalWorkTimeForUserID(int userId)
        {

            var dataFromDatabase = GetDataStopTimeFromDatabase();
            var dataUserId = dataFromDatabase.Where(p => p.UserID == userId);
            List<TimeSpan> timeSpans = [];
            Console.WriteLine($"\n\n * Data for user about ID: {userId}. \n\n");
            Console.WriteLine("   * Time for work on the project about: \n");
            foreach (var result in dataUserId)
            {
                TimeSpan timeSpan = result.TimeStop.Subtract(result.TimeStart);
                Console.WriteLine($"     * ID: {result.ProjectID} is {timeSpan}");
                timeSpans.Add(timeSpan);
            }
            long timeSpanTics = timeSpans.Sum(p => p.Ticks);
            TimeSpan timeSpanRaw = new TimeSpan(timeSpanTics);
            Console.WriteLine($"\n\n   * Total time on the projects for user about ID {userId} is: {timeSpanRaw.Days}D {timeSpanRaw.Hours}H {timeSpanRaw.Minutes}m {timeSpanRaw.Seconds}s \n\n\n\n\n\n\n\n\n");

            return timeSpanRaw;
        }

        public static TimeSpan DemoWorkTimeForUserIdAndProjectId(int userId,int projectId)
        {

            var dataFromDatabase = GetDataStopTimeFromDatabase();
            var dataUserId = dataFromDatabase.Where(p => p.UserID == userId).Where(p=>p.ProjectID==projectId);
            List<TimeSpan> timeSpans = [];
            Console.WriteLine($"\n\n * Data for user about ID: {userId} and project ID: {projectId}. \n\n");
            Console.WriteLine("   * Time of work for: \n");
            foreach (var result in dataUserId)
            {
                TimeSpan timeSpan = result.TimeStop.Subtract(result.TimeStart);
                Console.WriteLine($"     * of user about ID: {result.ProjectID} and of project about ID: {result.UserID} is {timeSpan}");
                timeSpans.Add(timeSpan);
            }
            long timeSpanTics = timeSpans.Sum(p => p.Ticks);
            TimeSpan timeSpanRaw = new TimeSpan(timeSpanTics);
            Console.WriteLine($"\n\n   * Total time for the projects obout ID: {projectId} and for the user about ID {userId} is: {timeSpanRaw.Days}D {timeSpanRaw.Hours}H {timeSpanRaw.Minutes}m {timeSpanRaw.Seconds}s \n\n\n\n\n\n\n\n\n");

            return timeSpanRaw;
        }






    }
}
