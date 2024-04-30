using ProjectClock.BusinessLogic.Dtos.Organization;
using ProjectClock.BusinessLogic.Dtos.Project.ProjectDtos;

namespace ProjectClock.BusinessLogic.Services.ExcelRaportServices;

public class GenerateDataDto
    {
        public int userId { get; set; }
        public int projectId { get; set; }
        public int organizationId { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public IEnumerable<ProjectDto> projects { get; set; } = new List<ProjectDto>();
        public IEnumerable<OrganizationDto> organizations { get; set; } = new List<OrganizationDto>();
    }

