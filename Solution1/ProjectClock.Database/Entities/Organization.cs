using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
