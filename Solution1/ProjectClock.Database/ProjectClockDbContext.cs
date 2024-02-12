using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProjectClock.Database.Entities;

namespace ProjectClock.Database
{
    public class ProjectClockDbContext: DbContext
    {

        public ProjectClockDbContext(DbContextOptions<ProjectClockDbContext> options): base(options)
        {
            
        }
        
        DbSet<User> Users { get; set; }
        DbSet<Project> Projects { get; set; }      

    }
}
