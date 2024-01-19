using ProjectClock.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Services.ProjectServices
{
    public class ProjectEditor : General
    {
        public static void ModifyProject(int id, string name)
        {
            var project = GetProject(id);
            SetID(project, id);
            SetName(project, name);
        }


    }
}
