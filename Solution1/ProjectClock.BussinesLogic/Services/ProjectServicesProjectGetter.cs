using ProjectClock.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Services
{
    internal class ProjectServicesProjectGetter
    {
        //hard coded list of projects in future should be get from json
        public static List<Project> GetProjectList()
        {
            List<Project> projects = new List<Project>()
            {
                new Project() { Id = 1, Name = "ToDoApp"},
                new Project() { Id = 2, Name = "Amazon Store"},
                new Project() { Id = 0, Name = "Internal office tasks"},
                new Project() { Id = 3, Name = "Internal marketing"}
            };
            return projects;
        }     
    }
}
