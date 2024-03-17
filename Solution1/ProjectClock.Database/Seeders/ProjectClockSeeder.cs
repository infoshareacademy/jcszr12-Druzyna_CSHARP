using ProjectClock.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.Database.Seeders
{
    public class ProjectClockSeeder
    {
        private ProjectClockDbContext _dbContext;

        public ProjectClockSeeder(ProjectClockDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            Organization org = new Organization() { Name = "IBM" };
            Organization org2 = new Organization() { Name = "Coca-Cola" };


            if (await _dbContext.Database.CanConnectAsync())
            {

                if (!_dbContext.Users.Any())
                {
                    User user = new User("Scottie", "Pippen", "scottie@gmail.com");
                    User user2 = new User("Michael", "Jordan", "jordan@gmail.com");
                    User user3 = new User("Luc", "Longley", "longley@gmail.com");
                    List<User> users = new List<User>() { user, user2, user3 };

                    await _dbContext.Users.AddRangeAsync(users);
                    await _dbContext.SaveChangesAsync();
                }

                if (!_dbContext.Projects.Any())
                {
                    Project project = new Project() { Name = "NBA.com", Organization = org };
                    Project project2 = new Project() { Name = "ShittyLand", Organization = org };
                    Project project3 = new Project() { Name = "PorkStattion", Organization = org2 };
                    List<Project> projects = new List<Project>() { project, project2, project3};

                    await _dbContext.Projects.AddRangeAsync(projects);
                    await _dbContext.SaveChangesAsync();
                }



            }
        }
    }
}
