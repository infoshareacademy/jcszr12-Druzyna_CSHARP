﻿using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.Database.Entities
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        private User? _owner;
        public List<User> Users { get; set; } = new List<User>();
        public List<Project> Projects { get; set; } = new List<Project>();

        public User Owner
        {
            get { return _owner; }
            set 
            {
                if (value != null && value.UserPosition == Position.Owner)
                {
                    _owner = value;
                }   
                else
                {
                    throw new ArgumentException("Owner must have the position of Owner.");
                }
            }
        }

    }
}
