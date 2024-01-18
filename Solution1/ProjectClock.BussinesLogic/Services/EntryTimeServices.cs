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

            return timeEnter;
        }

        public static TimeEnter Stop(string descriptionActivitysInProject, TimeEnter timeEnter)
        { 
            timeEnter.EndTime = DateTime.Now;
            timeEnter.Description = descriptionActivitysInProject;
            timeEnter.Time = timeEnter.EndTime - timeEnter.StartTime;

            return timeEnter;
        }

    }
}