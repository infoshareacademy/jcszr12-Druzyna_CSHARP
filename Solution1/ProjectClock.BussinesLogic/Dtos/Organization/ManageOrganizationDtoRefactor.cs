using ProjectClock.BusinessLogic.Dtos.Organization;
using ProjectClock.Database.Entities;

namespace ProjectClock.BusinessLogic.Dtos.OrganizationDto
{
    public class ManageOrganizationDtoRefactor
    {
        public int OrganizationId { get; set; }
        public int? SelectedOrganizationId { get; set; }
        public ICollection<int>? OrganizationIds { get; set; } = new List<int>();
        public string? OrganizationName { get; set; }
        public ICollection<string> OrganizationNames { get; set; }
        public string NewUserEmailInvitatation { get; set; }
        public ICollection<ChooseOrganizationDto> ChooseOrganizations { get; set; } = new List<ChooseOrganizationDto>();
        public ICollection<ChooseUserDto> ChooseUserDto { get; set; } = new List<ChooseUserDto>();
        public ICollection<string> OrganizationUserNames { get; set; } = new List<string>();

    }


}