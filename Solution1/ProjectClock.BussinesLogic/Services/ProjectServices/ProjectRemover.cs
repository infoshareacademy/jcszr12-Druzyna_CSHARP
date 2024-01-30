using ProjectClock.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Services.ProjectServices
{
    public class ProjectRemover : General
    {
        public static bool RemoveProject(int id)
        {
            var projects = ProjectGetter.GetProjectList();
            var projectsWithout = projects.Where(p => p.Id != id).ToList();
            if (projectsWithout.Count() == projects.Count())
            {
                return false;
            }
            var path = GetDirectoryToFileFromDataFolder("projects.json");

            
            ProjectSaver.Save(projectsWithout, path);
            return true;
        }


    }
}
