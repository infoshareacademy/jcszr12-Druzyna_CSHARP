using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.Database.Entities
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = default!;
        public List<UserProject> UserProjects { get; set; } = new List<UserProject>();
        public List<WorkingTime> WorkingTimes { get; set; } = new List<WorkingTime>();
        [ForeignKey("Organization")]
        public int OrganizationId { get; set; }
        public Organization? Organization { get; set; }
    }
}
