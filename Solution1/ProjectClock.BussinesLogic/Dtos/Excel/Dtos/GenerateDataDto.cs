namespace ProjectClock.BusinessLogic.Services.ExcelRaportServices;

public class GenerateDataDto
    {
        public int userId { get; set; }
        public int projectId { get; set; }
        public int organizationId { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
    }

