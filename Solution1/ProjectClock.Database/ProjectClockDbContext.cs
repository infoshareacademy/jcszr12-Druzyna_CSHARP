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
        
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }      

    }
}
