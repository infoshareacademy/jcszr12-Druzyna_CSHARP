using ProjectClock.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Services
{
    public class ProjectServicesCreateProject : ProjectServices
    {
        public static Project CreateProject()
        {
            var project = new Project();
            SetID(project);
            SetName(project);
            return project;
        }

        





    }
}
