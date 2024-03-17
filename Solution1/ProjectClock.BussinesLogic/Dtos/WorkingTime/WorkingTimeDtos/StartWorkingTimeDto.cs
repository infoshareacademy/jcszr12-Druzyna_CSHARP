using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Dtos.WorkingTime.WorkingTimeDtos
{
    public class StartWorkingTimeDto
    {
        public int UserId { get; set; }
        public string ProjectName { get; set; } 
    }
}
