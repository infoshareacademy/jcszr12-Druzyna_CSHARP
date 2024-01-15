using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Models
{
    internal class User
    {
        internal int ID { get; set; }
        internal string Name { get; set; }
        internal string Surname { get; set; }
        internal Position UserPosition { get; set; }
        internal TimeSpan TotalWorkTime { get; set; }
    }
}
