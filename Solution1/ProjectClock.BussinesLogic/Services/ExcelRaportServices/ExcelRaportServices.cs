using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectClock.BusinessLogic.Dtos.Excel;
using ProjectClock.BusinessLogic.Services.OrganizationServices;
using ProjectClock.BusinessLogic.Services.ProjectServices;
using ProjectClock.BusinessLogic.Services.UserServices;
using ProjectClock.BusinessLogic.Services.WorkingTimeServices;
using ProjectClock.Database;
using ProjectClock.Database.Entities;

namespace ProjectClock.BusinessLogic.Services.ExcelRaportServices;

public partial class ExcelRaportServices : IExcelRaportServices
{
    private readonly IProjectServices _projectServices;
    private readonly IWorkingTimeServices _workingTimeServices;
    private readonly ProjectClockDbContext _projectClockDbContext;


    public ExcelRaportServices(IProjectServices organizationServices, ProjectClockDbContext projectClockDbContext, IWorkingTimeServices workingTimeServices)
    {
        _projectServices = organizationServices;
        _projectClockDbContext = projectClockDbContext;
        _workingTimeServices = workingTimeServices;
    }

    public async Task<DataForExcelUserRaportDto> GenerateDataUser(GenerateDataUserDto dto)
    {
        var userProjects = await _projectServices.GetAllUserProjects(dto.userId);
        var user = _projectClockDbContext.Users.AsNoTracking().FirstOrDefault(u => u.Id == dto.userId);
        var data = new DataForExcelUserRaportDto()
        {
            UserName = user.Name,
            UserSurname = user.Surname,
            FromDate = dto.fromDate.ToString("dd-MM-yyyy"),
            ToDate = dto.toDate.ToString("dd-MM-yyyy"),
            GenerateDate = DateTime.Now.ToString("dd-MM-yyyy")
        };
        foreach (var project in userProjects)
        {
            var raportData = new ProjectWithTimeDto()
            {
                Name = project.Name ?? "-",
                OrganizationName = project.Organization ?? "-",
            };
            var worktimes = _projectClockDbContext.WorkingTimes
                .AsNoTracking()
                .Where(wt => wt.UserId == dto.userId
                        && wt.EndTime != null
                        && wt.ProjectId == project.Id
                        && wt.StartTime >= dto.fromDate
                        && wt.StartTime <= dto.toDate)
                .ToList();
            if(!worktimes.Any())
            {
                break;
            }
            foreach (var worktime in worktimes)
            {
                var workTimeTotal = worktime.EndTime - worktime.StartTime;
                raportData.TotalTime += (TimeSpan)workTimeTotal;
            }
            data.ProjectData.Add(raportData);
        }
        return data;
    }
    public async Task<DataForExcelProjectRaportDto> GenerateDataProject(GenerateDataProjectDto dto)
    {
        return new DataForExcelProjectRaportDto() { };
    }
    public async Task<DataForExcelOrganizationRaportDto> GenerateDataOrganization(GenerateDataOrganizationDto dto)
    {
        return new DataForExcelOrganizationRaportDto() { };
    }
}
