using ProjectClock.BusinessLogic.Dtos.Project.ProjectDtos;
using ProjectClock.Database.Entities;

namespace ProjectClock.BusinessLogic.Services.ProjectServices
{
    public interface IProjectServices
    {
        Task<bool> Create(CreateProjectDto project);
        Task<Project> GetById(int id);
        Task<IEnumerable<ProjectDto>> GetAll();
        Task Update(Project model);
        Task<bool> Delete(int id);
    }

}

