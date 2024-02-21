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
        public WorkingTime WorkingTime { get; set; }
        private readonly ProjectClockDbContext _projectClockDbContext;

        public SqlWorkingTime(ProjectClockDbContext projectClockDbContext)
        {
            _projectClockDbContext = projectClockDbContext;
        }

        public bool PushToSql(WorkingTime workingTime)
        {
            try
            {
                workingTime.ProjectName = workingTime.Project.Name;
                workingTime.UserName = workingTime.User.Name;

                //zaimplementować linijki dodające do Usera dany projekt :)

                _projectClockDbContext.WorkingTimes.Add(workingTime);
                _projectClockDbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool StartWork(WorkingTime workingTime)
        {
            var project = _projectClockDbContext.WorkingTimes.FirstOrDefault(w => w == workingTime);

            if (project != null)
            {
                try
                {
                    project.StartTime();
                    _projectClockDbContext.SaveChanges(true);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool StartWork(int id)
        {
            var project = _projectClockDbContext.WorkingTimes.FirstOrDefault(w => w.Id == id);

            if (project != null)
            {
                try
                {
                    project.StartTime();
                    _projectClockDbContext.SaveChanges(true);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool StopWork(WorkingTime workingTime)
        {
            var project = _projectClockDbContext.WorkingTimes.FirstOrDefault(w => w == workingTime);

            if (project != null)
            {
                try
                {
                    project.StopTime();
                    _projectClockDbContext.SaveChanges(true);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool StopWork(int id)
        {
            var project = _projectClockDbContext.WorkingTimes.FirstOrDefault(w => w.Id == id);

            if (project != null)
            {
                try
                {
                    project.StopTime();
                    _projectClockDbContext.SaveChanges(true);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }  
    }
}
