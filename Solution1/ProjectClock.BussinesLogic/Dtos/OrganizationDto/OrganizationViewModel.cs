using ProjectClock.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Dtos.OrganizationDto
{
    public class OrganizationViewModel
    {
        public List<Organization> Organizations { get; set; }
        public List<User> Users { get; set; }
        public List<Project> Projects { get; set; } = new List<Project>();
    }
}
