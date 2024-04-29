namespace ProjectClock.BusinessLogic.Dtos.Excel.Dtos;

public class DataForExcelOrganizationRaportDto
{
    public string UserName { get; set; } = string.Empty;
    public string OrganizationName { get; set; } = string.Empty;
    public string FromDate { get; set; } = string.Empty;
    public string ToDate { get; set; } = string.Empty;
    public string GenerateDate { get; set; } = string.Empty;
    public List<OrganizationWithTimeDto> OrganizationData { get; set; } = new List<OrganizationWithTimeDto>();
}
