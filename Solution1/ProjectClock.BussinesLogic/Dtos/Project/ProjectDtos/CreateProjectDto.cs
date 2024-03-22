using ProjectClock.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Dtos.Project.ProjectDtos
{
    public class CreateProjectDto
    {
       public int UserId { get; set; }
       public List<Organization> UserOrganizations {  get; set; }
       public string ProjectName { get; set; }
       public string OrganizationId { get; set; }
    }
}
