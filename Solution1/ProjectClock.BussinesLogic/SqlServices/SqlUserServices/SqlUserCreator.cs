using Microsoft.Identity.Client;
using ProjectClock.BusinessLogic.SqlServices.SqlUserServices.SqlUserInterfaces;
using ProjectClock.Database;
using ProjectClock.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.SqlServices.SqlUserServices
{
    public class SqlUserCreator: ISqlUserCreator
    {
        private readonly ProjectClockDbContext _projectClockDbContext;
        private readonly SqlUserGeneral _sqlUserGeneral;

        public SqlUserCreator(ProjectClockDbContext projectClockDbContext, SqlUserGeneral sqlUserGeneral)
        {
            _projectClockDbContext = projectClockDbContext;
            _sqlUserGeneral = sqlUserGeneral;
        }

        public bool Create(string userName, string surName)
        {
            try
            {
                User user = new User(surName, userName);
                _projectClockDbContext.Users.Add(user);
                _projectClockDbContext.SaveChanges();
                _sqlUserGeneral.SetId(user);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
