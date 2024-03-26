using ProjectClock.Database.Entities;

namespace ProjectClock.BusinessLogic.Dtos.OrganizationDto
{
    public class ManageOrganizationDto
    {
        public int OrganizationId { get; set; }
        public int? SelectedOrganizationId { get; set; }
        public Database.Entities.Organization Organization { get; set; }
        public User User { get; set; }
        public List<Database.Entities.Organization> Organizations { get; set; }
        public List<Database.Entities.User>? OrganizationUsers { get; set; } = new List<Database.Entities.User> ();
        public List<Database.Entities.Project>? Projects { get; set; } = new List<Database.Entities.Project>();
        public List<User>? AllUsers { get; set; } = new List<User>();

    }

   
}