using ProjectClock.Database;
using ProjectClock.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.SqlServices.SqlUserServices
{
    public class SqlUserGetter
    {
        private readonly ProjectClockDbContext _projectClockDbContext;
        private readonly SqlUserGeneral _sqlUserGeneral;

        public SqlUserGetter(ProjectClockDbContext projectClockDbContext, SqlUserGeneral sqlUserGeneral)
        {
            _projectClockDbContext = projectClockDbContext;
            _sqlUserGeneral = sqlUserGeneral;
        }

        public User Get(int id)
        {
            if (_sqlUserGeneral.UserExist(id, out User user))
            {
                return user;
            }
            else
            {
                return null;
            }            
        }

    }
}
