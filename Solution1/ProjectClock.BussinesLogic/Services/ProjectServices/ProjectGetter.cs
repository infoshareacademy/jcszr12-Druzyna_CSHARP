using ProjectClock.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace ProjectClock.BusinessLogic.Services.ProjectServices
{
    public class ProjectGetter : General
    {
        public static List<Project> GetProjectList()
        {
            var json = File.ReadAllText(GetDirectoryToFileFromDataFolder("projects.json"));
            List<Project> projects = JsonConvert.DeserializeObject<List<Project>>(json);
            return projects;
        }
    }
}
