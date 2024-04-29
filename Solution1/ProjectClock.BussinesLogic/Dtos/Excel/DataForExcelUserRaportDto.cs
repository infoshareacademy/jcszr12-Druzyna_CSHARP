using ProjectClock.BusinessLogic.Dtos.Project.ProjectDtos;

namespace ProjectClock.BusinessLogic.Dtos.Excel;

public class DataForExcelUserRaportDto
{
    public string UserName { get; set; }
    public string UserSurname { get; set; }
    public ProjectWithTimeDto ProjectData { get; set; }
}
