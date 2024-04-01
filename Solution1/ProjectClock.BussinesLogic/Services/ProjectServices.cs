using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectClock.BusinessLogic.Dtos.Project.ProjectDtos;
using ProjectClock.Database;
using ProjectClock.Database.Entities;

namespace ProjectClock.BusinessLogic.Services
{
    public interface IProjectServices
    {
        Task<bool> Create(CreateProjectDto project);
        Task<Project> GetById(int id);
        Task<IEnumerable<ProjectDto>> GetAll();
        Task Update(Project model);
        Task<bool> Delete(int id);
    }

    public class ProjectServices : IProjectServices
    {
        private ProjectClockDbContext _projectClockDbContext;
        private IMapper _mapper;

        public ProjectServices(ProjectClockDbContext projectClockDbContext, IMapper mapper)
        {
            _projectClockDbContext = projectClockDbContext;
            _mapper = mapper;
        }

        public async Task<bool> Create(CreateProjectDto dto)
        {
            if (await _projectClockDbContext.Projects
            .AsNoTracking()
            .AnyAsync(p => p.Name == dto.ProjectName 
                && p.Organization.Name == dto.OrganizationName))
                {                  
                    return false;
                }

            var project = new Project()
            {
                Name = dto.ProjectName,
                Organization = await _projectClockDbContext.Organizations.SingleOrDefaultAsync(e => e.Name == dto.OrganizationName),
            };

            await _projectClockDbContext.Projects.AddAsync(project);
            await _projectClockDbContext.SaveChangesAsync();
            return true;
        }

        

        public async Task<Project?> GetById(int id)
        {
            return await _projectClockDbContext.Projects.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<ProjectDto>> GetAll()
        {
            var list = await _projectClockDbContext.Projects.Include(p => p.Organization).ToListAsync();

            var dtos = _mapper.Map<IEnumerable<ProjectDto>>(list);

            return dtos;
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

        
    }

}

