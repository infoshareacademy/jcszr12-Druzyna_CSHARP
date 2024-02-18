using ProjectClock.BusinessLogic.SqlServices.SqlProjectServices.SqlProjectInterfaces;
using ProjectClock.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.SqlServices.SqlProjectServices
{
    public class SqlProjectEditor : ISqlProjectEditor
    {
        private readonly ProjectClockDbContext _projectClockDbContext;
        private readonly SqlProjectGetter _sqlProjectGetter;
        private readonly SqlProjectGeneral _sqlProjectGeneral;

        public SqlProjectEditor(ProjectClockDbContext projectClockDbContext, SqlProjectGetter sqlProjectGetter, SqlProjectGeneral sqlProjectGeneral)
        {
            _projectClockDbContext = projectClockDbContext;
            _sqlProjectGetter = sqlProjectGetter;
            _sqlProjectGeneral = sqlProjectGeneral;
        }

        public bool Modify(int oldId, int newId, string newName)
        {
            var project = _projectClockDbContext.Projects.FirstOrDefault(p => p.Id == oldId);
            bool newIdExist = _sqlProjectGeneral.Exist(newId);

            if (project == null || newIdExist)
            {
                return false;                
            }

            try
            {
                project.Id = newId;
                project.Name = newName;
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
