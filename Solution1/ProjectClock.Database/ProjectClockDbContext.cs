﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProjectClock.BusinessLogic.Models;
using ProjectClock.BusinessLogic.Models.DataTimeRecorder;

namespace ProjectClock.Database
{
    public class ProjectClockDbContext: DbContext
    {

        public ProjectClockDbContext(DbContextOptions<ProjectClockDbContext> options): base(options)
        {
            
        }

        private readonly string _connectionString = @"Data Source=DESKTOP-0D8TD9S;Database=ProjectClockDb;Integrated Security=True;TrustServerCertificate=True";
        DbSet<User> Users { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<StartWork> StartWorks { get; set; }
        DbSet<StopWork> StopWorks { get; set; }


      



    }
}
