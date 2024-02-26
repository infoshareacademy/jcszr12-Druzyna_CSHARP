﻿using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.Database.Entities
{
    public class Organisation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private User? _owner;
        public List<User> Users { get; set; } = new List<User>();
        public List<Project> Projects { get; set; } = new List<Project>();
        public List<WorkingTime> WorkingTimes { get; set; } = new List<WorkingTime>();
    }
}
