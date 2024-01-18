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
        public static void CreateProject()
        {

            var path = GetDirectoryToFile("projects.json");
            var projects = ProjectServicesProjectGetter.GetProjectList();
            var project = new Project();
            
            SetID(project);
            SetName(project); 
            projects.Add(project);
            ProjectServicesProjectSaver.Save(projects,path);                     
        }

        





    }
}
