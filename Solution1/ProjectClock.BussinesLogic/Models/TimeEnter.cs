using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Models
{
    public class TimeEnter
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Project Project { get; set; }
        public User User { get; set; }  
        public string Description { get; set; }
        public TimeSpan Time {  get; set; }
    }
}
