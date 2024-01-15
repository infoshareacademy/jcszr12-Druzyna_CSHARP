using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Models
{
    internal class TimeEnter
    {
        internal DateTime StartTime { get; set; }
        internal DateTime EndTime { get; set; }
        internal Project Project { get; set; }
        internal string Description { get; set; }
        internal TimeSpan Time {  get; set; }
    }
}
