using ProjectClock.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProjectClock.BusinessLogic.Services
{
    internal class ProjectServicesProjectGetter
    {
        public static List<Project> GetProjectList()
        {
            var json = File.ReadAllText(@"d:\00_InfoShare Academy\000_Project\Solution1\ProjectClock.BussinesLogic\Data\projects.json");
            List<Project> projects = JsonConvert.DeserializeObject<List<Project>>(json);
            return projects;
        }     
    }
}
