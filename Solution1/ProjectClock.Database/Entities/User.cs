using System.ComponentModel.DataAnnotations;

namespace ProjectClock.Database.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }     
        public List<UserProject> UserProjects { get; set; } = new List<UserProject>();
        public List<WorkingTime> WorkingTimes { get; set; } = new List<WorkingTime>();
        
        public List<OrganizationUser> OrganizationUsers { get; set; }

        public User(string name, string surname, string email)
        {
            Name = name;
            Surname = surname;
            Email = email;
        }

    }

}
