namespace ProjectClock.BusinessLogic.Dtos.Organization
{
    public class InvitationToOrganizationDto
    {
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public ICollection<OrganizationDto> InvitingOrganizations { get; set; } = new List<OrganizationDto>();

    }
}
