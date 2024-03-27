using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Dtos.Organization
{
    public class DeleteOrganizationDto
    {
        public int OrganizationId { get; set; }
        public Database.Entities.Organization Organization { get; set; }
        public ICollection<Database.Entities.Organization> Organizations { get; set; } = new List<Database.Entities.Organization>();
    }
}
