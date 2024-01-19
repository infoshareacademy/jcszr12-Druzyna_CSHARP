using ProjectClock.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectClock.BusinessLogic.Services
{
    public class ProjectServicesProjectCreator : ProjectServices
    {
        public static void CreateProject(string name)
        {

            var path = GetDirectoryToFile("projects.json");
            var projects = ProjectServicesProjectGetter.GetProjectList();
            var project = new Project();
            
            SetID(project, projects.Count+1);
            SetName(project, name); 
            projects.Add(project);
            ProjectServicesProjectSaver.Save(projects,path);                     
        }

        





    }
}
