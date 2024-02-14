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
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<WorkingTime> WorkingTime { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<WorkingTime>()
        //        .OwnsOne(w => w.Project);

        //    modelBuilder.Entity<WorkingTime>()
        //        .OwnsOne(w => w.User);

        //    modelBuilder.Entity<WorkingTime>()
        //        .Property(w => w.StartTime);
        //}

        public DbSet<WorkingTime> WorkingTimes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkingTime>()
                .Property(wt => wt.Time)
                .HasConversion(
                    v => v.ToString(), // Konwersja TimeSpan na string
                    v => TimeSpan.Parse(v) // Konwersja string na TimeSpan
                );
        }
    }

}

