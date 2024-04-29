using ProjectClock.BusinessLogic.Dtos.Excel;

namespace ProjectClock.BusinessLogic.Services.ExcelServices
{
    public interface IExcelServices
    {
        MemoryStream GenerateExcelForOrganization(string templatePath, DataForExcelOrganizationRaportDto dto);
        MemoryStream GenerateExcelForProject(string templatePath, DataForExcelProjectRaportDto dto);
        MemoryStream GenerateExcelForUser(string templatePath, DataForExcelUserRaportDto dto);
    }
}