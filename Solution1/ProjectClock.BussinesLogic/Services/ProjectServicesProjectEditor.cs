using ProjectClock.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Services
{
    public class ProjectServicesProjectEditor : ProjectServices
    {
        public static void ModifyProject(int id)
        {          
            var project = GetProject(id);
            SetID(project);
            SetName(project);
            var project1 = GetProject(3);
        }

        
    }
}
