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
        Task<bool> ProjectExist(string name);
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
                if (await ProjectExist(project.Name))
                {
                    throw new Exception($"This project already exist");
                    return false;

                }
                else
                {
                    _projectClockDbContext.Projects.Add(project);
                    await _projectClockDbContext.SaveChangesAsync();
                    return true;

                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Create(string name)
        {
            try
            {
                if (await ProjectExist(name))
                {
                    throw new Exception($"This project already exist");
                    return false;

                }
                else
                {
                    Project project = new Project() { Name = name };
                    _projectClockDbContext.Projects.Add(project);
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
            var list = await _projectClockDbContext.Projects.ToListAsync();
            return list;
        }

        public async Task<List<Project>> GetAllUserProjects()
        {
            var list = await _projectClockDbContext.Projects.ToListAsync();
            return list;
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

        public async Task<bool> ProjectExist(string name)
        {
            return await _projectClockDbContext.Projects.AsNoTracking().AnyAsync(p => p.Name == name);
        }
    }

}

