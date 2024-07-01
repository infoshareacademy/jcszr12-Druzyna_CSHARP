using Microsoft.EntityFrameworkCore;
using ProjectClock.Database;
using ProjectClock.Database.Entities;

namespace ProjectClock.BusinessLogic.Services.OrganizationUserServices
{
    public class OrganizationUserServices : IOrganizationUserServices
    {
        private ProjectClockDbContext _projectClockDbContext;
        public OrganizationUserServices(ProjectClockDbContext projectClockDbContext)
        {
            _projectClockDbContext = projectClockDbContext;
        }

        public bool IsUserAnOwner(int userId)
        {
            int numOfProjects = _projectClockDbContext.OrganizationsUsers.Count(ou => ou.UserId == userId);

            return numOfProjects > 0;
        }

        public async Task<IEnumerable<Organization>> GetUserOrganizations(int userId)
        {
            var userOrganizations =
                await _projectClockDbContext.OrganizationsUsers.Where(ou => ou.UserId == userId).Select(ou => ou.Organization).ToListAsync();

            return userOrganizations;
        }

        public async Task<IEnumerable<User>> GetOrganizationUsers(int organizationId)
        {
            var organizationUsers = await _projectClockDbContext.OrganizationsUsers
                .Where(ou => ou.OrganizationId == organizationId).Select(ou => ou.User).ToListAsync();

            return organizationUsers;
        }


    }
}