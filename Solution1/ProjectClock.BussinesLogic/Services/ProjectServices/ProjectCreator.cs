using ProjectClock.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectClock.BusinessLogic.Services.ProjectServices
{
    public class ProjectCreator : General
    {
        public static void CreateProject(string name)
        {

            var path = GetDirectoryToFileFromDataFolder("projects.json");
            var projects = ProjectGetter.GetProjectList();
            var project = new Project();
            var idList = projects.Select(p => p.Id).ToList();
            var i = 0;
            while (i == idList[i])
            {
                i++;
            }
            var id = i;
            SetID(project, id);
            SetName(project, name);
            projects.Add(project);
            ProjectSaver.Save(projects, path);
        }







    }
}
