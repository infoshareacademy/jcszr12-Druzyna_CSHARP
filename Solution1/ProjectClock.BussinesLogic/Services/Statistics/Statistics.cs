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


        public static TimeSpan TotalTimeForProjectID(int projectId)
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

        public static TimeSpan TotalTimeForUserID(int userId)
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





        public static TimeSpan DemoTotalTimeForProjectID(int projectId)
        {

            var dataFromDatabase = GetDataStopTimeFromDatabase();
            var dataProjectId = dataFromDatabase.Where(p => p.ProjectID == projectId);
            List<TimeSpan> timeSpans = [];
            Console.WriteLine($"\n\n * Data for projects about ID: {projectId} \n");
            Console.WriteLine("   * Time for users working on the project about: \n");
            foreach (var result in dataProjectId)
            {
                TimeSpan timeSpan = result.TimeStop.Subtract(result.TimeStart);
                Console.WriteLine($"     * ID: {result.UserID} is {timeSpan}");
                timeSpans.Add(timeSpan);
            }
            long timeSpanTics = timeSpans.Sum(p => p.Ticks);
            TimeSpan timeSpanRaw = new TimeSpan(timeSpanTics);
            Console.WriteLine($"\n\n  * Total time for the projects about ID {projectId} is: {timeSpanRaw.Days}D {timeSpanRaw.Hours}H {timeSpanRaw.Minutes}m {timeSpanRaw.Seconds}s \n\n\n\n");

            return timeSpanRaw;
        }

        public static TimeSpan DemoTotalTimeForUserID(int userId)
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






    }
}
