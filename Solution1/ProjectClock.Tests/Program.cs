using Microsoft.EntityFrameworkCore;
using ProjectClock.BusinessLogic.Services;
using ProjectClock.Database.Entities;
using ProjectClock.Database;

namespace ProjectClock.Tests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProjectClockDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CarWorkshopDb;Trusted_Connection=True;");

            // Inicjalizacja DbContext z opcjami
            using (var dbContext = new ProjectClockDbContext(optionsBuilder.Options))
            {
                //User user = new User("Barack", "Obama", "barack@whitehouse.com");
                //Project project = new Project() { Name = "Retirement" };
                //WorkingTime workingTime = new WorkingTime() { Project = project, User = user, };

                UserServices userServices = new UserServices(dbContext);
                ProjectServices projecServices = new ProjectServices(dbContext);
                WorkingTimeServices workingTimeServices = new WorkingTimeServices(dbContext);


              

                //sqlWorkingTime.PushToSql(workingTime);

                //sqlWorkingTime.StartWork(1);
                ////sqlWorkingTime.StopWork(1);

                //User user2 = new User("John", "Lenon", "john@wp.pl");
                //sqlUserCreator.Create(user2);
                //sqlUserCreator.Create("Keith", "Flint", "keith@wp.pl");

                //Project project2 = new Project() { Name = "The Beatles" };
                //sqlProjectCreator.Create(project2.Name);


                User user3 = new User("Maxim3", "JustMaxim3", "maxim3@wp.pl");
                sqlUserCreator.Create(user3);
                Project project = new Project() { Name = "Solo Album" };
                sqlProjectCreator.Create(project.Name);
                user3.Projects.Add(project);
                dbContext.SaveChanges();

                //var workingTime = dbContext.WorkingTime.FirstOrDefault(w => w.Id == 2);

                //workingTime.StartTime();

                //dbContext.SaveChanges();


                Console.WriteLine("End");
            }
        }
    }
}
