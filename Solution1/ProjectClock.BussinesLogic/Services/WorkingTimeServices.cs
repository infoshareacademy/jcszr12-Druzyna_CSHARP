using Microsoft.EntityFrameworkCore;
using ProjectClock.BusinessLogic.Dtos.WorkingTime.WorkingTimeDtos;
using ProjectClock.Database;
using ProjectClock.Database.Entities;

namespace ProjectClock.BusinessLogic.Services
{

    public class WorkingTimeServices : IWorkingTimeServices
    {
        private ProjectClockDbContext _projectClockDbContext;

        public WorkingTimeServices(ProjectClockDbContext projectClockDbContext)
        {
            _projectClockDbContext = projectClockDbContext;
        }

        public async Task<bool> Create(StartStopWorkingTimeDto dto)
        {
            if (WorkingTimeExist(dto))
            {
                return false;
            }
            var project = await _projectClockDbContext.Projects.FirstOrDefaultAsync(p => p.Name == dto.ProjectName);
            var user = await _projectClockDbContext.Users.FirstOrDefaultAsync(u => u.Id == dto.UserId);

            var workingTime = new WorkingTime()
            {
                Project = project,
                User = user,
                StartTime = DateTime.UtcNow,
            };
                await _projectClockDbContext.WorkingTimes.AddAsync(workingTime);
                await _projectClockDbContext.SaveChangesAsync();

                return true;
        }

        public async Task<WorkingTime?> GetById(int id)
        {
            return await _projectClockDbContext.WorkingTimes.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<WorkingTime>> GetAll()
        {
            return await _projectClockDbContext.WorkingTimes.ToListAsync();
        }

        public async Task Update(UpdateWorkingTimeDto dto)
        {
            var workingTime = await _projectClockDbContext.WorkingTimes.FirstOrDefaultAsync();

           

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

        public bool WorkingTimeExist(StartStopWorkingTimeDto dto)
        {
            return _projectClockDbContext.WorkingTimes.AsNoTracking().Any(wt =>
                wt.Project.Name == dto.ProjectName && wt.User.Id == dto.UserId);
        }

        public bool WorkingTimeExist(int id)
        {
            return _projectClockDbContext.WorkingTimes.AsNoTracking().Any(wt => wt.Id == id);
        }

        public async Task<bool> StopWork(WorkingTime workingTime)
        {
            try
            {
                int id = workingTime.Id;

                if (!WorkingTimeExist(id))
                {
                    throw new Exception($"This record of WorkingTime doesn't exist");
                    return false;
                }
                else if (workingTime.StartTime is not null)
                {
                    throw new Exception($"This record of WorkingTime hasn't started");
                    return false;
                }
                else
                {
                    workingTime.EndTime = DateTime.UtcNow;
                    workingTime.TotalWorkTime = workingTime.EndTime - workingTime.StartTime;
                    await _projectClockDbContext.SaveChangesAsync();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<int> GetId(WorkingTime workingTime)
        {
            var wt = _projectClockDbContext.WorkingTimes.FirstOrDefaultAsync(wt =>
                wt.Project.Name == workingTime.Project.Name && wt.User.Email == workingTime.User.Email);

            return wt.Id;
        }

    }

}

