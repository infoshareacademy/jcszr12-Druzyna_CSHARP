namespace ProjectClock.BusinessLogic.Dtos.Organization
{
    public class DeleteOrganizationDto
    {
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public ICollection<OrganizationDto> Organizations { get; set; } = new List<OrganizationDto>();
        
    }
}
