using ProjectClock.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Services
{
    public class ProjectServicesEntryTime : ProjectServices
    {
        public static TimeEnter Start(int id) 
        {
            var project = GetProject(id);
           
            var te = new TimeEnter();
            te.StartTime = DateTime.Now;
            te.Project = project;

            Console.WriteLine($"\nSelected project: {project.Name} ");
            Console.WriteLine($"\nStart time of working on the project {DateTime.Now} ");


            return te;


        }

        public static TimeEnter Stop( string description )
        {

            var te = new TimeEnter();
            te.EndTime = DateTime.Now;
            te.Description = description;
             
            Console.WriteLine($"\n Stop Selected project: {te.EndTime} ");

            return te;
        }

        public static TimeEnter CalculateTime(DateTime startTime, DateTime stopTime)
        {
            TimeSpan diff = stopTime - startTime;
            var te = new TimeEnter();
            te.Time = diff;


            
            
           // Console.WriteLine($"\n Stop Selected project: {diff.TotalHours} hours ");

            return te; 
        }
    }
}