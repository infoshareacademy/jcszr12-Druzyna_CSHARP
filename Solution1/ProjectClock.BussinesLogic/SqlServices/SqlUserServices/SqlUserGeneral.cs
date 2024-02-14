using ProjectClock.BusinessLogic.SqlServices.SqlUserServices.SqlUserInterfaces;
using ProjectClock.Database;
using ProjectClock.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.SqlServices.SqlUserServices
{
    public class SqlUserGeneral :ISqlUserGeneral
    {
        private readonly ProjectClockDbContext _projectClockDbContext;        

        public SqlUserGeneral(ProjectClockDbContext projectClockDbContext)
        {
            _projectClockDbContext = projectClockDbContext;
            
        }

        public bool UserExist(int id, out User user)
        {
            user = _projectClockDbContext.Users.FirstOrDefault(u=>u.Id == id);

            if (user is null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IdExist(int id)
        {
            return _projectClockDbContext.Users.Any(u => u.Id == id);
        }

        public void SetId(User user)
        {
            var availableId = Enumerable.Range(1, int.MaxValue).Except(_projectClockDbContext.Users.Select(u => u.Id)).FirstOrDefault();
            user.Id = availableId;
            _projectClockDbContext.SaveChanges();

        }

        public bool SetUserPosition (int id, Position position)
        {
            User user = _projectClockDbContext.Users.FirstOrDefault(u=>u.Id == id);

            if (user is null)
            {
                return false;
            }           

            user.UserPosition = position;
            _projectClockDbContext.SaveChanges();
            return true;
        }

    }
}
