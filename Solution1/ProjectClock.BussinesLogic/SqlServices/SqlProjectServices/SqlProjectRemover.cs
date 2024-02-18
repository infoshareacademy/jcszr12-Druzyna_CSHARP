using ProjectClock.BusinessLogic.SqlServices.SqlProjectServices.SqlProjectInterfaces;
using ProjectClock.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.SqlServices.SqlProjectServices
{
    public class SqlProjectRemover : ISqlProjectRemover
    {
        private readonly ProjectClockDbContext _projectClockDbContext;
        private readonly SqlProjectGeneral _sqlProjectGeneral;
        private readonly SqlProjectGetter _sqlProjectGetter;

        public SqlProjectRemover(ProjectClockDbContext projectClockDbContext, SqlProjectGeneral sqlProjectGeneral, SqlProjectGetter sqlProjectGetter)
        {
            _projectClockDbContext = projectClockDbContext;
            _sqlProjectGeneral = sqlProjectGeneral;
            _sqlProjectGetter = sqlProjectGetter;
        }

        public bool Remove(int id) 
        {
            var project = _projectClockDbContext.Projects.FirstOrDefault(p => p.Id == id);

            if (project == null)
            {
                throw new Exception("Project not found");
            }

            try
            {
                _projectClockDbContext.Projects.Remove(project);
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
