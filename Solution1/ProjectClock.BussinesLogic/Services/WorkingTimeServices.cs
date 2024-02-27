using Microsoft.EntityFrameworkCore;
using ProjectClock.Database;
using ProjectClock.Database.Entities;

namespace ProjectClock.BusinessLogic.Services
{
    public interface IWorkingTimeServices
    {
        Task<bool> Create(WorkingTime workingTime);
        Task<WorkingTime>? GetById(int id);
        Task<List<WorkingTime>> GetAll();
        Task Update(WorkingTime model);
        Task<bool> Delete(int id);
        bool WorkingTimeExist(WorkingTime workingTime);
    }

    public class WorkingTimeServices : IWorkingTimeServices
    {
        private ProjectClockDbContext _projectClockDbContext;

        public WorkingTimeServices(ProjectClockDbContext projectClockDbContext)
        {
            _projectClockDbContext = projectClockDbContext;
        }

        public async Task<bool> Create(WorkingTime workingTime)
        {
            try
            {
                
                if (WorkingTimeExist(workingTime))
                {
                    throw new Exception($"There is record with the same User and Project");
                    return false;
                }
                else
                {
                    string userEmail = workingTime.User.Email;
                    var existingUser = await _projectClockDbContext.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
                    
                    if (existingUser != null)
                    {
                        workingTime.User = existingUser;
                    }


                    string projectName = workingTime.ProjectName;
                    var existingProject = await _projectClockDbContext.Projects.FirstOrDefaultAsync(p=>p.Name == projectName);

                    if (existingProject != null)
                    {
                        workingTime.Project = existingProject;
                    }

                    await _projectClockDbContext.WorkingTimes.AddAsync(workingTime);
                    await _projectClockDbContext.SaveChangesAsync();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<WorkingTime?> GetById(int id)
        {
            return await _projectClockDbContext.WorkingTimes.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<WorkingTime>> GetAll()
        {
            return await _projectClockDbContext.WorkingTimes.ToListAsync();
        }

        public async Task Update(WorkingTime model)
        {
            var workingTime = await GetById(model.Id);

            workingTime.StartTime = model.StartTime;
            workingTime.EndTime = model.EndTime;
            workingTime.Description = model.Description;

            await _projectClockDbContext.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
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
                    var wt = await GetById(id);
                    _projectClockDbContext.WorkingTimes.Remove(wt);
                    await _projectClockDbContext.SaveChangesAsync();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool WorkingTimeExist(WorkingTime workingTime)
        {
            return _projectClockDbContext.WorkingTimes.AsNoTracking().Any(wt =>
                wt.Project.Name == workingTime.Project.Name && wt.User.Email == workingTime.User.Email);
        }

        public bool WorkingTimeExist(int id)
        {
            return _projectClockDbContext.WorkingTimes.AsNoTracking().Any(wt => wt.Id == id);
        }


    }

}

