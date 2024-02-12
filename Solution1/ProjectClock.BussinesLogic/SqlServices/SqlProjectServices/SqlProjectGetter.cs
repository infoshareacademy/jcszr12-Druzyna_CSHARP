﻿using ProjectClock.BusinessLogic.SqlServices.SqlProjectServices.SqlProjectInterfaces;
using ProjectClock.Database;
using ProjectClock.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.SqlServices.SqlProjectServices
{
    public class SqlProjectGetter: ISqlProjectGetter
    {
        private readonly ProjectClockDbContext _projectClockDbContext;

        public SqlProjectGetter(ProjectClockDbContext projectClockDbContext)
        {
            _projectClockDbContext = projectClockDbContext;
        }

        public Project? GetProject(int id)
        {
           return _projectClockDbContext.Projects.FirstOrDefault(p=>p.Id == id);
        }

        public List<Project> GetProjectList()
        {
            return _projectClockDbContext.Projects.ToList();
        }
    }
}
