using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProjectClock.Database.Entities;

namespace ProjectClock.Database
{
    public class ProjectClockDbContext : DbContext
    {

        public ProjectClockDbContext(DbContextOptions<ProjectClockDbContext> options) : base(options)
        {

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<WorkingTime> WorkingTimes { get; set; }
        public DbSet<Organization> Organizations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<WorkingTime>()
                .Property(wt => wt.TotalWorkTime)
                .HasConversion(
                    v => v.ToString(), // Konwersja TimeSpan na string
                    v => TimeSpan.Parse(v) // Konwersja string na TimeSpan
                );

            modelBuilder.Entity<User>(eb =>
            {
                eb.HasMany(u => u.WorkingTimes)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId);

            });


            modelBuilder.Entity<Project>(eb =>
            {
                eb.HasMany(u => u.WorkingTimes)
                .WithOne(u => u.Project)
                .HasForeignKey(u => u.ProjectId);

            });

            modelBuilder.Entity<OrganizationUser>().HasKey(x => new { x.UserId, x.OrganizationId });
           
        }
    }
}

