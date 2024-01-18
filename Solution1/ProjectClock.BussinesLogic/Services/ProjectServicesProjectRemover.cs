using ProjectClock.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Services
{
    public class ProjectServicesProjectRemover : ProjectServices
    {
        public static void RemoveProject(int id)
        {
            var projects = ProjectServicesProjectGetter.GetProjectList();
            var project = GetProject(id);
            var path = GetDirectoryToFile("projects.json");

            projects.Remove(project);
            Console.WriteLine($"{project.Name} deleted!");
            ProjectServicesProjectSaver.Save(projects, path);
        }


    }
}
