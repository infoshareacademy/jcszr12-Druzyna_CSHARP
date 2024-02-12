using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.Database.Entities
{
    public class WorkingTime
    {
        public int Id { get; set; }
        public Project Project { get; set; } = default!;
        public User User { get; set; } = default!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Time { get;  private set; }
        public string? Description { get; set; }

        //public void EncodeName() => EncodedName = Name.ToLower().Replace(" ", "-");

        public TimeSpan CalculateWorkingTime() => EndTime - StartTime;

    }
}
