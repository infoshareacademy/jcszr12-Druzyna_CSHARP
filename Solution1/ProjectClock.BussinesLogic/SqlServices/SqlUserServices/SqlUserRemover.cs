using ProjectClock.BusinessLogic.SqlServices.SqlUserServices.SqlUserInterfaces;
using ProjectClock.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.SqlServices.SqlUserServices
{
    internal class SqlUserRemover: ISqlUserRemover
    {
        private readonly ProjectClockDbContext _projectClockDbContext;
        private readonly SqlUserGeneral _sqlUserGeneral;

        public SqlUserRemover(ProjectClockDbContext projectClockDbContext, SqlUserGeneral sqlUserGeneral )
        {
            _projectClockDbContext = projectClockDbContext;
            _sqlUserGeneral = sqlUserGeneral;
        }

        public bool Remove(int id)
        {
            var user = _projectClockDbContext.Users.FirstOrDefault(u => u.Id == id);

            if (user != null)
            {
                try
                {
                    _projectClockDbContext.Users.Remove(user);
                    _projectClockDbContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                   return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool RemoveAll() 
        {
            var users = _projectClockDbContext.Users.ToList();

            if (users != null)
            {
                try
                {
                    _projectClockDbContext.Users.RemoveRange(users);
                    _projectClockDbContext.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
                
            }
            else
            {
                return false;
            }
        }

    }
}
