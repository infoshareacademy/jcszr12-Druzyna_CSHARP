using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectClock.Database;
using ProjectClock.Database.Entities;

namespace ProjectClock.BusinessLogic.Services
{
    public interface IWorkingTimeServices
    {
        bool Create(WorkingTime workingTime);
        WorkingTime? GetById(int id);
        List<WorkingTime> GetAll();
        void Update(WorkingTime model);
        bool Delete(int id);
        bool WorkingTimeExist(int id);
    }

    public class WorkingTimeServices : IWorkingTimeServices
    {
        private ProjectClockDbContext _projectClockDbContext;

        public WorkingTimeServices(ProjectClockDbContext projectClockDbContext)
        {
            _projectClockDbContext = projectClockDbContext;
        }

        public bool Create(WorkingTime workingTime)
        {
            try
            {
                if (WorkingTimeExist(workingTime.Id))
                {
                    throw new Exception($"This record of WorkingTime already exist");
                    return false;
                }
                else
                {
                    _projectClockDbContext.WorkingTimes.Add(workingTime);
                    _projectClockDbContext.SaveChanges();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public WorkingTime? GetById(int id)
        {
            return _projectClockDbContext.WorkingTimes.FirstOrDefault(u => u.Id == id);
        }

        public List<WorkingTime> GetAll()
        {
            return _projectClockDbContext.WorkingTimes.ToList();
        }

        public void Update(WorkingTime model)
        {
            var workingTime = GetById(model.Id);

            workingTime.StartTime = model.StartTime;
            workingTime.EndTime = model.EndTime;
            workingTime.Description = model.Description;
        }

        public bool Delete(int id)
        {
            try
            {
                if (!WorkingTimeExist(id))
                {
                    throw new Exception($"This record of WorkingTime doesn't exist");
                    return false;
                }
                else
                {
                    var wt = GetById(id);
                    _projectClockDbContext.WorkingTimes.Remove(wt);
                    _projectClockDbContext.SaveChanges();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool WorkingTimeExist(int id)
        {
            return _projectClockDbContext.WorkingTimes.Any(u => u.Id == id);
        }
    }

}

