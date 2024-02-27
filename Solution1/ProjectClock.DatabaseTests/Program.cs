using Microsoft.EntityFrameworkCore;
using ProjectClock.BusinessLogic.Services;
using ProjectClock.Database;
using ProjectClock.Database.Entities;

namespace ProjectClock.DatabaseTests
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProjectClockDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ProjectClock;Trusted_Connection=True;");

            // Inicjalizacja DbContext z opcjami
            using (var dbContext = new ProjectClockDbContext(optionsBuilder.Options))
            {
                UserServices userServices = new UserServices(dbContext);
                ProjectServices projectServices = new ProjectServices(dbContext);
                WorkingTimeServices workingTimeServices = new WorkingTimeServices(dbContext);


                User user = new User("Scottie", "Pippen", "scottie@gmail.com");
                User user2 = new User("Michael", "Jordan", "jordan@gmail.com");
                User user3 = new User("Luc", "Lonlgey", "longley@gmail.com");
                Project project = new Project() { Name = "NBA.com" };
                Project project2 = new Project() { Name = "ShittyLand" };
                Project project3 = new Project() { Name = "PorkStattion" };
                WorkingTime wt = new WorkingTime() { User = user, Project = project, ProjectName = project.Name, UserName = user.Name };

                await userServices.Create(user);
                await userServices.Create(user2);
                await userServices.Create(user3);
                await projectServices.Create(project);
                await projectServices.Create(project2);
                await projectServices.Create(project3);
                await workingTimeServices.Create(wt);


                Console.WriteLine("End");
            }

        }
    }
}
