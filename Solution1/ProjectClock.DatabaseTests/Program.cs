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
                //OrganizationServices organizationService = new OrganizationServices(dbContext);
               
                Organization org = new Organization() { Name = "BigOrganiaztion" };
                Organization org2 = new Organization() { Name = "SmallOrganiaztion" };
                Project project = new Project() { Name = "NBA.com", Organization = org };
                User user = new User("Scottie", "Pippen", "scottie@gmail.com");
                user.UserProjects.Add(new UserProject() {Project = project});
                User user2 = new User("Michael", "Jordan", "jordan@gmail.com");
                User user3 = new User("Luc", "Longley", "longley@gmail.com");
                
                Project project2 = new Project() { Name = "ShittyLand", Organization = org };
                Project project3 = new Project() { Name = "PorkStattion", Organization = org };
              
                WorkingTime wt = new WorkingTime() { User = user, Project = project};
                WorkingTime wt2 = new WorkingTime() { User = user2, Project = project2};


                //await userServices.Create(user);
                //await userServices.Create(user2);
                //await userServices.Create(user3);
                //await projectServices.Create(project);
                //await projectServices.Create(project2);
                //await projectServices.Create(project3);
                //await workingTimeServices.Create(wt);
                //await organizationService.Create(org2);

                //User user4 = new User("John", "Hancock", "hancock@gmail.com");
                //Project project4 = new Project() { Name = "SRC Keybord" };
                //await workingTimeServices.Create(wt2);
                Console.WriteLine("End");
            }

        }
    }
}
