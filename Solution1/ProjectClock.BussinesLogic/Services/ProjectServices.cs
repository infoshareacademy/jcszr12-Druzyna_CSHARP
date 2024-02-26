using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectClock.Database;
using ProjectClock.Database.Entities;

namespace ProjectClock.BusinessLogic.Services
{
    public interface IProjectServices
    {
        bool Create(Project project);
        Project? GetById(int id);
        List<Project> GetAll();
        void Update(Project model);
        bool Delete(int id);
        bool ProjectExist(int id);
    }

    public class ProjectServices : IProjectServices
    {
        private ProjectClockDbContext _projectClockDbContext;

        public ProjectServices(ProjectClockDbContext projectClockDbContext)
        {
            _projectClockDbContext = projectClockDbContext;
        }

        public bool Create(Project project)
        {
            try
            {
                if (ProjectExist(project.Id))
                {
                    throw new Exception($"This project already exist");
                    return false;
                }
                else
                {
                    _projectClockDbContext.Projects.Add(project);
                    _projectClockDbContext.SaveChanges();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public Project? GetById(int id)
        {
            return _projectClockDbContext.Projects.FirstOrDefault(u => u.Id == id);
        }

        public List<Project> GetAll()
        {
            return _projectClockDbContext.Projects.ToList();
        }

        public void Update(Project model)
        {
            var project = GetById(model.Id);
            project.Name = model.Name;
        }

        public bool Delete(int id)
        {
            try
            {
                if (!ProjectExist(id))
                {
                    throw new Exception($"Project with {id} doesn't exist");
                    return false;
                }
                else
                {
                    var project = GetById(id);
                    _projectClockDbContext.Projects.Remove(project);
                    _projectClockDbContext.SaveChanges();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool ProjectExist(int id)
        {
            return _projectClockDbContext.Users.Any(u => u.Id == id);
        }
    }

}

