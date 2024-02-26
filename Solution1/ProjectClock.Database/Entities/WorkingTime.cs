using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.Database.Entities
{
    public class WorkingTime
    {
        public int Id { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string ProjectName { get; set; }
        public string UserName { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public TimeSpan TotalWorkTime { get; private set; }
        public string? Description { get; set; }

    }
}
