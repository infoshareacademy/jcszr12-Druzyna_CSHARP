using ProjectClock.BusinessLogic.Models;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Services
{
    public class EntryTimeServices :ProjectServices
    {
        public static TimeEnter Start(int idOfProject)
        {
            var timeEnter = new TimeEnter();
            timeEnter.StartTime = DateTime.Now;
            timeEnter.Project = GetProject(idOfProject);

            //Console.WriteLine($"\n Selected project ID: {project.Id} ");
            //Console.WriteLine($"\n Selected project name: {project.Name} ");
            //Console.WriteLine($"\n Start time of working on the project {DateTime.Now} ");

            return timeEnter;
        }

        public static TimeEnter Stop(string descriptionActivitysInProject, TimeEnter timeEnter)
        { 
            timeEnter.EndTime = DateTime.Now;
            timeEnter.Description = descriptionActivitysInProject;

            //Console.WriteLine($"\n End of working time on the project: {timeEnter.EndTime} ");
            //Console.WriteLine($"\n Description activities in project : {timeEnter.Description} ");

            return timeEnter;
        }

        public static TimeEnter CalculateTime(TimeEnter timeEnter)
        {
            var project = new Project();
            project.TotalProjectTime = timeEnter.EndTime - timeEnter.StartTime;
            timeEnter.Project = project;

            //Console.WriteLine($"\n Working time on the project: {timeEnter.Project.TotalProjectTime} ");
            
            return timeEnter; 
        }
    }
}