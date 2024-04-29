using ProjectClock.BusinessLogic.Dtos.Excel;

namespace ProjectClock.BusinessLogic.Services.ExcelRaportServices
{
    public interface IExcelRaportServices
    {
        Task<DataForExcelOrganizationRaportDto> GenerateDataOrganization(GenerateDataOrganizationDto dto);
        Task<DataForExcelProjectRaportDto> GenerateDataProject(GenerateDataProjectDto dto);
        Task<DataForExcelUserRaportDto> GenerateDataUser(ExcelRaportServices.GenerateDataUserDto dto);
    }
}