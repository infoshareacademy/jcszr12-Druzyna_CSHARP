using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.Database.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        public Position UserPosition { get; set; }
        public List<Project> Projects { get; set; } = new List<Project>();
        public List<WorkingTime> WorkingTimes { get; set; } = new List<WorkingTime>();


        public User(string name, string surname, string email)
        {
            Name = name;
            Surname = surname;
            Email = email;
        }

    }

}
