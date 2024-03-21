namespace ProjectClock.Database.Entities
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<OrganizationUser> OrganizationUsers { get; set; }
        public List<Project> Projects { get; set; } = new List<Project>();
    }
}
