namespace ProjectClock.BusinessLogic.Services.ExcelRaportServices;

public partial class ExcelRaportServices
{
    public class GenerateDataUserDto() 
    {
        public int userId { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
    }
}
