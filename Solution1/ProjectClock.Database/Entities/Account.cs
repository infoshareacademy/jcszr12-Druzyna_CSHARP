using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.Database.Entities
{
    public class Account
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string PasswordHash { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public User AccountProfile { get; set; }

    }
}
