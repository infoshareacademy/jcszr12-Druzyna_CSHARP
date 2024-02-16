using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.Database.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public List<User> Users { get; set; } = new List<User>();
        public List<WorkingTime> WorkingTimes { get; set; } = new List<WorkingTime>();
    }
}
