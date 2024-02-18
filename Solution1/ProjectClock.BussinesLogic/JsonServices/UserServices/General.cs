using Newtonsoft.Json;
using ProjectClock.BusinessLogic.Models;
using ProjectClock.BusinessLogic.Services.ProjectServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Services.UserServices
{
    public class General
    {
        public static User GetUser(int id)
        {
            List<User> users = GetUserList();
            var user = users.FirstOrDefault(u => u.Id == id); 
            if (user == null)
            {
                return null;
            }
            return user; 
        }

        public static List<User> GetUserList()
        {
            var path = Services.GetDirectoryToFileFromDataFolder("users.json");
            var jsonDeser = File.ReadAllText(path);
            var users = JsonConvert.DeserializeObject<List<User>>(jsonDeser);
            return users;
        }
        public static void Save(List<User> users,string path)
        {
            var json = JsonConvert.SerializeObject(users);
            File.WriteAllText(path, json);
        }

        public static Position? GetUserPosition(string name, string surnname)
        {
            List<User> users = GetUserList();
            var user = users.FirstOrDefault(u => u.Name == name && u.Surname == surnname);
            if (user == null) 
            {
                return null;
            }

            return user.UserPosition;
        }

        public static int GetUserIdByNameAndSurname(string name, string surnname)
        {
            List<User> users = GetUserList();
            var user = users.FirstOrDefault(u => u.Name == name && u.Surname == surnname);
            return user.Id;
        }
    }
}

