namespace ProjectClock.BusinessLogic.Dtos.WorkingTime.WorkingTimeDtos
{
    public class UpdateWorkingTimeDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime {get; set;}
        public string Description {get; set;}
    }
}