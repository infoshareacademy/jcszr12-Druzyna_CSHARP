using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Services.UserServices
{
    public class AccessService : General
    {
        public static bool IsLogged(string name, string surrname)
        {
            var users = GetUserList();
            var isLogged = users.Any(u => u.Name == name && u.Surname == surrname );
            return isLogged;
        }
    }
}
