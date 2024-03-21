using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Dtos.WorkingTime.WorkingTimeDtos
{
    public class NotFinisedWorkingTimeDto
    {
        public int WorkingTimeId { get; set; }
        public string ProjectName { get; set; }
        public bool IsFinished { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
