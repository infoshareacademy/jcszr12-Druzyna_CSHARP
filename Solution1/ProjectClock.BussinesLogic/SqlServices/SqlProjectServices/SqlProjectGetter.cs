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
    public class SqlProjectGetter: ISqlProjectGetter
    {
        private readonly ProjectClockDbContext _projectClockDbContext;

        public SqlProjectGetter(ProjectClockDbContext projectClockDbContext)
        {
            _projectClockDbContext = projectClockDbContext;
        }

        public Project GetProject(int id)
        {
            throw new NotImplementedException();
        }

        public List<Project> GetProjectList()
        {
            throw new NotImplementedException();
        }
    }
}
