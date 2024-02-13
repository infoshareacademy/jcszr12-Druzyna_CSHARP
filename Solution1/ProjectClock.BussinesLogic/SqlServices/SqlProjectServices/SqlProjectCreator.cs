using Microsoft.EntityFrameworkCore;
using ProjectClock.BusinessLogic.SqlServices.SqlProjectServices.SqlProjectInterfaces;
using ProjectClock.Database;
using ProjectClock.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.SqlServices.SqlProjectServices
{
    public class SqlProjectCreator : ISqlProjectCreator
    {
        private readonly ProjectClockDbContext _projectClockDbContext;

        public SqlProjectCreator(ProjectClockDbContext projectClockDbContext)
        {
            _projectClockDbContext = projectClockDbContext;
        }

        public bool Create(string name)
        {
            try
            {
                List<int> ids = _projectClockDbContext.Projects.Select(p => p.Id).ToList();

                int firstFreeId = 1;

                while (ids.Contains(firstFreeId))
                {
                    firstFreeId++;
                }

                Project project = new Project()
                {
                    Id = firstFreeId,
                    Name = name,
                };

                _projectClockDbContext.Projects.Add(project);
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
