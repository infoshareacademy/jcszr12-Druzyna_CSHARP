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
        public DateTime _startTime;
        private DateTime _endTime;
        private TimeSpan _time;
        public string? Description { get; set; }

        public DateTime EndTime
        {
            get { return _endTime; }
            private set
            {
                if (value < _startTime)
                {
                    throw new ArgumentException("EndTime cannot be earlier than StartTime.");
                }
                else if (_startTime == DateTime.MinValue)
                {
                    throw new ArgumentException("StartTime must be set before setting EndTime.");
                }
                else
                {
                    _endTime = value;
                    _time = _endTime - _startTime;
                }
            }
        }

        public WorkingTime()
        {

        }
        public void StartTime()
        {
            _startTime = DateTime.Now;
        }

        public void StopTime() 
        { 
            _endTime = DateTime.Now;            
        }

    }
}
