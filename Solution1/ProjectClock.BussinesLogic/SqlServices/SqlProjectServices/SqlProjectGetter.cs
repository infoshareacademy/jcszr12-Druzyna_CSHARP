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
    public class SqlProjectGetter : ISqlProjectGetter
    {
        private readonly ProjectClockDbContext _projectClockDbContext;
        private readonly SqlProjectGeneral _sqlProjectGeneral;

        public SqlProjectGetter(ProjectClockDbContext projectClockDbContext, SqlProjectGeneral sqlProjectGeneral)
        {
            _projectClockDbContext = projectClockDbContext;
            _sqlProjectGeneral = sqlProjectGeneral;
        }

        public Project? GetProject(int id)        
        {
            if (!_sqlProjectGeneral.Exist(id))
            { 
                throw new Exception("Id not found");
            }

            return _projectClockDbContext.Projects.FirstOrDefault(p => p.Id == id);
        }

        public List<Project> GetProjectList()
        {
            return _projectClockDbContext.Projects.ToList();
        }
    }
}
