using Newtonsoft.Json.Linq;
using ProjectClock.BusinessLogic.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Services.ProjectServices
{
    public class ProjectEditor : General
    {
        public static bool ModifyProject(int id,int idNew, string nameNew)
        {
            var projects = ProjectGetter.GetProjectList();          
            var path = GetDirectoryToFileFromDataFolder("projects.json");

            var project = projects.FirstOrDefault(x => x.Id == id);
            if (project != null)
            {
                SetID(project, idNew);
                SetName(project, nameNew);
                ProjectSaver.Save(projects, path);
                return true;
            }
            return false;
        }
    }
}
