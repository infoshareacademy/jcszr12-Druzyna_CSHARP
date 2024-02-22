using ProjectClock.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.SqlServices.SqlUserServices.SqlUserInterfaces
{
    public class ISqlUserInterfaces
    {

    }
    public interface ISqlUserGeneral
    {
        bool UserExist(int id, out User user);
        bool IdExist(int id);
        void SetId(User user);
        bool SetUserPosition(int id, Position position);
    }

    public interface ISqlUserGetter
    {
        User Get(int id);
        List<User> GetUsers();
    }

    public interface ISqlUserRemover
    {
        bool Remove(int id);
        bool RemoveAll();

    }

    public interface ISqlUserCreator
    {
        bool Create(string userName, string surName, string email);
        bool Create(User user);
    }


}