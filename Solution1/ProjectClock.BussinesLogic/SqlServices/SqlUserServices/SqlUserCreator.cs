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
    public class SqlUserCreator : ISqlUserCreator
    {
        private readonly ProjectClockDbContext _projectClockDbContext;
        private readonly SqlUserGeneral _sqlUserGeneral;

        public SqlUserCreator(ProjectClockDbContext projectClockDbContext, SqlUserGeneral sqlUserGeneral)
        {
            _projectClockDbContext = projectClockDbContext;
            _sqlUserGeneral = sqlUserGeneral;
        }

        public bool Create(string userName, string surName, string email)
        {
            try
            {
                User user = new User(userName, surName, email);

                if (_sqlUserGeneral.UserExist(email))
                {
                    throw new Exception($"User with email {email} already exist");
                    return false;
                }
                else
                {
                    _projectClockDbContext.Users.Add(user);
                    _projectClockDbContext.SaveChanges();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Create(User user)
        {
            try
            {
                if (_sqlUserGeneral.UserExist(user.Email))
                {
                    throw new Exception($"User with email {user.Email} already exist");
                    return false;
                }
                else
                {
                    _projectClockDbContext.Users.Add(user);
                    _projectClockDbContext.SaveChanges();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }



    }
}
