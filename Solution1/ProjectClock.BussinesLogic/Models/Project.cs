using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Models
{
    internal class Project
    {
        internal int Id { get; set; }
        internal string Name { get; set; }
        internal TimeSpan TotalProjectTime { get; set; }
    }
}
