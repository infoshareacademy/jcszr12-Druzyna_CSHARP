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
        public Project Project { get; set; } = default!;
        public User User { get; set; } = default!;
        public string ProjectName { get; set; }
        public string UserName { get; set; } 
        public DateTime? StartWork { get; set; }
        public TimeSpan TotalWorkTime { get; private set; }
        public string? Description { get; set; }
        public DateTime? EndWork { get; set; }        
            
        public void StartTime()
        {
            StartWork = DateTime.Now;
        }
        public void StopTime()
        {
            DateTime currentTime = DateTime.Now;

            if (currentTime < StartWork)
            {
                throw new InvalidOperationException("EndWork cannot be earlier than StartWork.");
            }
            else if (StartWork is null)
            {
                throw new InvalidOperationException("StartWork is not set.");
            }
            else
            {
                EndWork = currentTime;
                CalculateTotalWorkTime();
            }          
        }
        private void CalculateTotalWorkTime()
        {
            if (StartWork.HasValue && EndWork.HasValue)
            {
                TotalWorkTime = EndWork.Value - StartWork.Value;
            }
            else
            {
                TotalWorkTime = TimeSpan.Zero;
            }
        }


    }
}
