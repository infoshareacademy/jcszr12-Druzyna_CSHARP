using ProjectClock.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Services
{
    public class ProjectServicesModifyProject : ProjectServices
    {
        public static void ModifyProject(Project project)
        {           
            SetID(project);
            SetName(project);           
        }
    }
}
