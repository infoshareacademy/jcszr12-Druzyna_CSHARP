using ProjectClock.Database;
using ProjectClock.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.SqlServices.SqlWorkingTimeServices
{
    public class SqlWorkingTime     
    {
        private readonly ProjectClockDbContext _projectClockDbContext;

        public SqlWorkingTime(ProjectClockDbContext projectClockDbContext)
        {
            _projectClockDbContext = projectClockDbContext;
        }
        public WorkingTime WorkingTime { get; set; }       

        public void StartWork()
        {
            DateTime dateTime = DateTime.Now;
            WorkingTime.StartTime = dateTime;
        }

        public void StopWork() 
        { 
            DateTime dateTime = DateTime.Now;
            WorkingTime.EndTime = dateTime;
        }

        public bool PushToSql(WorkingTime workingTime)
        {
            try
            {
                _projectClockDbContext.WorkingTime.Add(workingTime);
                _projectClockDbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;              
            }
        }


    }
}
