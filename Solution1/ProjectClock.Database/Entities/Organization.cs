namespace ProjectClock.Database.Entities
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<OrganizationUser> OrganizationUsers { get; set; } = new List<OrganizationUser>();
        public List<Project> Projects { get; set; } = new List<Project>();
    }
}
