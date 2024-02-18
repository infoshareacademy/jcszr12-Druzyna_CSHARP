using ProjectClock.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ProjectClock.BusinessLogic.Services.ProjectServices;

namespace ProjectClock.BusinessLogic.Services.EntryTimeServices
{
    public class EntryTimeServices : General
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