using ProjectClock.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Services
{
    public class ProjectServices : Services
    {
        internal static void SetName(Project project)
        {
            bool isRight = false;
            string name = "";

            while (!isRight)
            {
                Console.WriteLine("Insert project name:");
                name = Console.ReadLine();
                if (name != "" && name != null) isRight = true;
            }
            project.Name = name;
        }

        internal static void SetID(Project project)
        {
            bool isRight = false;
            int Id = 1;

            while (!isRight)
            {
                Console.WriteLine("Insert project ID:");
                isRight = int.TryParse(Console.ReadLine(), result: out Id);               
            }
            project.Id = Id;
        }
        internal static Project GetProject(int id)
        {
            List<Project> list = ProjectServicesProjectGetter.GetProjectList();           
            var project = list.FirstOrDefault( e => e.Id == id);
            if(project == null)
            {
                Console.WriteLine("Project doesn't found!");
                return null;
            }
            Console.WriteLine($"Project {project.Name} founded");
            return project;
               
            
        }
    }
}
