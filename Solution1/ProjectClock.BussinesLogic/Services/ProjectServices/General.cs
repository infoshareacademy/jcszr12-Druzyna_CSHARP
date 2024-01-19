using ProjectClock.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Services.ProjectServices
{
    public class General : Services
    {
        internal static bool ExistProject(int id)
        {
            List<Project> list = ProjectGetter.GetProjectList();
            foreach (var item in list)
            {
                if (item.Id == id)
                {
                    return true;
                }
            }
            return false;

        }
        internal static void SetName(Project project, string name)
        {
            project.Name = name;
        }

        internal static void SetID(Project project, int id)
        {
            if (ExistProject(id))
            {
                project.Id = id;
            }

        }
        internal static Project GetProject(int id)
        {
            List<Project> list = ProjectGetter.GetProjectList();
            var project = list.FirstOrDefault(e => e.Id == id);
            if (project == null)
            {
                return null;
            }
            return project;


        }
    }
}
