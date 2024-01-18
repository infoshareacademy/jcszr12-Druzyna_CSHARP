using ProjectClock.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using System.Text.Json.Nodes;
using Newtonsoft.Json;

namespace ProjectClock.BusinessLogic.Services
{
    public class ProjectServicesProjectSaver
    {
        public static void Save(List <Project> projects,string path)
        {                                
            string json = JsonConvert.SerializeObject(projects);
            File.WriteAllText(path, json);           
            Console.WriteLine("saving...");
        }
    }
}
