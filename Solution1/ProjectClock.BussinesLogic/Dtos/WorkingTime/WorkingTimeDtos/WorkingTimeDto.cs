namespace ProjectClock.BusinessLogic.Dtos.WorkingTime.WorkingTimeDtos
{
    public class WorkingTimeDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ProjectName { get; set; }
        public bool IsFinished { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
