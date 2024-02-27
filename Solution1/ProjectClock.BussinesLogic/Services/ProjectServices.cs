using Microsoft.EntityFrameworkCore;
using ProjectClock.Database;
using ProjectClock.Database.Entities;

namespace ProjectClock.BusinessLogic.Services
{
    public interface IProjectServices
    {
        Task<bool> Create(Project project);
        Task<Project> GetById(int id);
        Task<List<Project>> GetAll();
        Task Update(Project model);
        Task<bool> Delete(int id);
        bool ProjectExist(string name);
    }

    public class ProjectServices : IProjectServices
    {
        private ProjectClockDbContext _projectClockDbContext;

        public ProjectServices(ProjectClockDbContext projectClockDbContext)
        {
            _projectClockDbContext = projectClockDbContext;
        }

        public async Task<bool> Create(Project project)
        {
            try
            {
                if (ProjectExist(project.Name))
                {
                    throw new Exception($"This project already exist");
                    return false;

                }
                else
                {
                    await _projectClockDbContext.Projects.AddAsync(project);
                    await _projectClockDbContext.SaveChangesAsync();
                    return true;

                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Project?> GetById(int id)
        {
            return await _projectClockDbContext.Projects.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Project>> GetAll()
        {
            return await _projectClockDbContext.Projects.ToListAsync();
        }

        public async Task Update(Project model)
        {
            var project = await GetById(model.Id);
            project.Name = model.Name;

            _projectClockDbContext.Projects.Update(project);
            await _projectClockDbContext.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var project = await GetById(id);

                if (project is null)
                {
                    throw new Exception($"Project with {id} doesn't exist");
                }
                else
                {
                    _projectClockDbContext.Projects.Remove(project);
                    await _projectClockDbContext.SaveChangesAsync();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool ProjectExist(string name)
        {
            return _projectClockDbContext.Projects.AsNoTracking().Any(p => p.Name == name);
        }
    }

}

