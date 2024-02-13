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
    public class SqlProjectGeneral : ISqlProjectGeneral
    {
        private readonly ProjectClockDbContext _projectClockDbContext;

        public SqlProjectGeneral(ProjectClockDbContext projectClockDbContext)
        {
            _projectClockDbContext = projectClockDbContext;
        }      

        public bool Exist(int id)
        {
            return _projectClockDbContext.Projects.Any(p => p.Id == id);
        }
    }
}
