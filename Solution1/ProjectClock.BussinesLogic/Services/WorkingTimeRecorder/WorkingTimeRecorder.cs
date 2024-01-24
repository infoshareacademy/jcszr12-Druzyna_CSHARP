using Newtonsoft.Json;
using ProjectClock.BusinessLogic.Models;
using ProjectClock.BusinessLogic.Models.DataTimeRecorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Services.WorkingTimeRecorder
{
    public class WorkingTimeRecorder
    {
        public static TimeSpan TotalTimeForProjectID(int projectId)
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

        public static TimeSpan TotalTimeForUserID(int userId)
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

        public static void WriteToDatabaseSimulationData()
        {
            CheckIfFileExistIfNotCreate();

            for (int i = 1; i <= 10; i++)
            {
                Random rndu = new Random();
                int usersId = rndu.Next(1, 20);

                Random rndp = new Random();
                int projectId = rndu.Next(1, 20);

                DataTimeRecorderStart dataTimeRecorderStart = new()
                {
                    UserID = usersId,
                    ProjectID = projectId,
                    TimeStart = DateTime.Now
                };
                WriteStartDataToRecorder(dataTimeRecorderStart);
            }

        }


        public static void ClearDatabase()
        {
            var dataTimeStartList = GetDataStartTimeFromDatabase().ToList();
            var dataTimeStopList = GetDataStopTimeFromDatabase().ToList();

            dataTimeStartList.Clear();
            dataTimeStopList.Clear();

            WriteToStartTimeDatabase(dataTimeStartList, GetDirectoryToFileFromDataFolder("recordsOfTimeStart.json"));
            WriteToStopTimeDatabase(dataTimeStopList, GetDirectoryToFileFromDataFolder("recordsOfTimeStop.json"));

            Console.Clear();
            Console.WriteLine($"\n\n * Database is empty \n");
        }


        public static void StartWork(int userId, int projectId)
        {
            CheckIfFileExistIfNotCreate();

            if (CheckIfProjectIsOpen(userId, projectId) == false)
            {
                DataTimeRecorderStart dataTimeRecorderStart = new DataTimeRecorderStart();
                dataTimeRecorderStart.UserID = userId;
                dataTimeRecorderStart.ProjectID = projectId;
                dataTimeRecorderStart.TimeStart = DateTime.Now;
                WriteStartDataToRecorder(dataTimeRecorderStart);
                Console.Clear();
                Console.WriteLine($"\n\n * Project about ID {projectId} for user about ID: {userId} is open and you can work. \n");
            }

        }


        private static bool CheckIfProjectIsOpen(int userId, int projectId)
        {
            var dataFromStartTimeDatabase = GetDataStartTimeFromDatabase();
            var ProjectToClose = dataFromStartTimeDatabase.Where(p => p.UserID == userId).Where(p => p.ProjectID == projectId);
            bool projectexist = false;
            foreach (var item in dataFromStartTimeDatabase)
            {
                if (item.UserID == userId && item.ProjectID == projectId)
                {
                    Console.Clear();
                    Console.WriteLine($"\n\n * Project about ID {projectId} for user about ID: {userId} is in progres. Choose another one. \n");
                    projectexist = true;
                }
                else
                {
                    projectexist = false;
                }
            }
            return projectexist;
        }


        public static void ViewProjectsInProgress()
        {
            var dataFromDatabase = GetDataStartTimeFromDatabase();

            Console.Clear();
            Console.WriteLine($"\n\n\n\n * All Projects in progress.\n");
            if (dataFromDatabase.Count == 0)
                Console.WriteLine("      - No projects in progress");
            foreach (var result in dataFromDatabase)
            {
                Console.WriteLine($"     - UserID: {result.UserID}, Project ID {result.ProjectID}");
            }
        }


        public static void ViewClosedProjects()
        {
            var dataFromDatabase = GetDataStopTimeFromDatabase();

            Console.Clear();
            Console.WriteLine($"\n\n\n\n * All closed projects.\n");
            if (dataFromDatabase.Count == 0)
                Console.WriteLine("      - No closed projects");
            foreach (var result in dataFromDatabase)
            {
                Console.WriteLine($"     - UserID: {result.UserID}, Project ID {result.ProjectID}");
            }
        }


        public static void StopWork(int userId, int projectId)
        {

            var dataFromStartTimeDatabase = GetDataStartTimeFromDatabase();
            var ProjectToClose = dataFromStartTimeDatabase.Where(p => p.UserID == userId).Where(p => p.ProjectID == projectId);



            if (ProjectToClose.Count() == 0)
            {
                Console.WriteLine($"\n * No projects to close for Project ID {projectId} and user about ID: {userId}. \n");
            }

            if (dataFromStartTimeDatabase.Count() == 0)
            {
                Console.WriteLine("\n * No projects to close.\n");
            }

            foreach (var result in ProjectToClose)
            {
                if (result.ProjectID == projectId && result.UserID == userId)
                {
                    DataTimeRecorderStop dataTimeRecorderStop = new()
                    {
                        UserID = userId,
                        ProjectID = projectId,
                        TimeStart = result.TimeStart,
                        TimeStop = DateTime.Now,
                    };
                    WriteStopDataToRecorder(dataTimeRecorderStop);
                    TimeSpan timeSpan = DateTime.Now - result.TimeStart;

                    Console.WriteLine($"\n * Work time for Project ID {projectId} and user about ID {userId} is: {timeSpan} \n");
                    Console.WriteLine($"\n * Project ID {projectId} for user about ID: {userId} is closed. \n");

                }

            }

            var startTimeList = dataFromStartTimeDatabase.ToList();
            startTimeList.RemoveAll(o => o.UserID == userId && o.ProjectID == projectId);
            Console.WriteLine($"\n * The number of remaining open projects is: {startTimeList.Count}\n\n\n");
            WriteToStartTimeDatabase(startTimeList, GetDirectoryToFileFromDataFolder("recordsOfTimeStart.json"));
        }


        private static string GetDirectoryToFileFromDataFolder(string fileName)
        {
            var pathOrigin = Directory.GetCurrentDirectory();
            var index = pathOrigin.IndexOf("Solution1");
            var pathPart = pathOrigin.Substring(0, index);
            var path = $@"{pathPart}\Solution1\ProjectClock.BussinesLogic\Data\WorkingTimeRecorder\{fileName}";
            return path;
        }


        private static void WriteStartDataToRecorder(DataTimeRecorderStart dataTimeRecorderStart)
        {


            var dataStartTimeRecordsFromDataBase = GetDataStartTimeFromDatabase();
            DataTimeRecorderStart dataTimeRecordsStart = dataTimeRecorderStart;

            if (dataStartTimeRecordsFromDataBase == null)
            {
                List<DataTimeRecorderStart> whenFileIsEmpty = [dataTimeRecorderStart];
                WriteToStartTimeDatabase(whenFileIsEmpty, GetDirectoryToFileFromDataFolder("recordsOfTimeStart.json"));
            }
            else
            {
                dataStartTimeRecordsFromDataBase.Add(dataTimeRecordsStart);
                WriteToStartTimeDatabase(dataStartTimeRecordsFromDataBase, GetDirectoryToFileFromDataFolder("recordsOfTimeStart.json"));
            }

        }


        private static void WriteStopDataToRecorder(DataTimeRecorderStop dataTimeRecorderStop)
        {


            var dataStopTimeRecordsFromDataBase = GetDataStopTimeFromDatabase();
            DataTimeRecorderStop dataTimeRecordsStop = dataTimeRecorderStop;

            if (dataStopTimeRecordsFromDataBase == null)
            {
                List<DataTimeRecorderStop> whenFileIsEmpty = [dataTimeRecorderStop];
                WriteToStopTimeDatabase(whenFileIsEmpty, GetDirectoryToFileFromDataFolder("recordsOfTimeStop.json"));
            }
            else
            {
                dataStopTimeRecordsFromDataBase.Add(dataTimeRecordsStop);
                WriteToStopTimeDatabase(dataStopTimeRecordsFromDataBase, GetDirectoryToFileFromDataFolder("recordsOfTimeStop.json"));
            }

        }


        private static List<DataTimeRecorderStart> GetDataStartTimeFromDatabase()
        {
            var json = File.ReadAllText(GetDirectoryToFileFromDataFolder("recordsOfTimeStart.json"));
            List<DataTimeRecorderStart> dataTimeRecords = JsonConvert.DeserializeObject<List<DataTimeRecorderStart>>(json);
            return dataTimeRecords;
        }


        private static List<DataTimeRecorderStop> GetDataStopTimeFromDatabase()
        {
            var json = File.ReadAllText(GetDirectoryToFileFromDataFolder("recordsOfTimeStop.json"));
            List<DataTimeRecorderStop> dataTimeRecords = JsonConvert.DeserializeObject<List<DataTimeRecorderStop>>(json);
            return dataTimeRecords;
        }


        private static void WriteToStartTimeDatabase(List<DataTimeRecorderStart> dtr, string path)
        {
            string json = JsonConvert.SerializeObject(dtr);
            File.WriteAllText(path, json);
        }


        private static void WriteToStopTimeDatabase(List<DataTimeRecorderStop> dtr, string path)
        {
            string json = JsonConvert.SerializeObject(dtr);
            File.WriteAllText(path, json);
        }


        private static void CheckIfFileExistIfNotCreate()
        {
            if (!File.Exists(GetDirectoryToFileFromDataFolder("recordsOfTimeStart.json")))
            {
                File.Create(GetDirectoryToFileFromDataFolder("recordsOfTimeStart.json"));
            }

            if (!File.Exists(GetDirectoryToFileFromDataFolder("recordsOfTimeStop.json")))
            {
                File.Create(GetDirectoryToFileFromDataFolder("recordsOfTimeStop.json"));
            }

        }


    }
}
