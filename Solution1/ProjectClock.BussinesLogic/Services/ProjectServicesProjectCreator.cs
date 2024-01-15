using ProjectClock.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Services
{
    public class ProjectServicesProjectCreator : ProjectServices
    {
        public static void CreateProject()
        {
            var projects = ProjectServicesProjectGetter.GetProjectList();
            var project = new Project();
            SetID(project);
            SetName(project); 
            projects.Add(project);
            ProjectServicesProjectSaver.Save(projects);
            //saving projects list to json... - soon           
        }

        





    }
}
