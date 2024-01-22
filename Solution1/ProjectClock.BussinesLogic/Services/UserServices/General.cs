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
            var project = users.FirstOrDefault(u => u.Id == id);
            if (project == null)
            {
                return null;
            }
            return project;
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
    }
}

