using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using ProjectClock.Database;
using ProjectClock.Database.Entities;

namespace ProjectClock.BusinessLogic.Services.OrganizationUserServices
{
    public class OrganizationUserServices : IOrganizationUserService
    {
        private ProjectClockDbContext _projectClockDbContext;
        public OrganizationUserServices(ProjectClockDbContext projectClockDbContext)
        {
            _projectClockDbContext = projectClockDbContext;
        }

        public bool IsUserAnOwner(int userId) //nie do końca dobre
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

        public async Task<bool> IsUserSignedToOrganization(int userId, int organizationId)
        {
            return _projectClockDbContext.OrganizationsUsers.Any(ou =>
                ou.UserId == userId && ou.OrganizationId == organizationId);
        }

        public async Task<IEnumerable<Organization>> GetUserAsAOwnerOrganization(int userId)
        {
            var userOrganizations =
                await _projectClockDbContext.OrganizationsUsers.Where(ou => ou.UserId == userId && ou.Role == Position.Owner || ou.Role == Position.Manager).Select(ou => ou.Organization).ToListAsync();

            return userOrganizations;
        }

        public async Task<bool> RemoveUserFromOrganization(int userId, int organizationId)
        {
            try
            {
                var ouToRemove = _projectClockDbContext.OrganizationsUsers.FirstOrDefault(ou =>
                    ou.OrganizationId == organizationId && ou.UserId == userId);

                if (ouToRemove is null)
                {
                    return false;
                }
                else
                {
                    _projectClockDbContext.OrganizationsUsers.Remove(ouToRemove);
                    await _projectClockDbContext.SaveChangesAsync();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AdvanceUserToManager(int userId, int organizationId)
        {
            try
            {
                var ouToAdvance = _projectClockDbContext.OrganizationsUsers.FirstOrDefault(ou =>
                    ou.OrganizationId == organizationId && ou.UserId == userId);

                if (ouToAdvance is null)
                {
                    return false;
                }
                else if (ouToAdvance.Role == Position.Owner || ouToAdvance.Role == Position.Manager)
                {
                    return false;
                }
                else
                {
                    ouToAdvance.Role = Position.Manager;
                    await _projectClockDbContext.SaveChangesAsync();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> IsUserAnOwnerOfParticularOrganization(int userId, int organizationId)
        {
            try
            {
                var ou = _projectClockDbContext.OrganizationsUsers.FirstOrDefault(ou =>
                    ou.UserId == userId && ou.OrganizationId == organizationId);

                if (ou is null)
                {
                    return false;
                }
                else if (ou.Role == Position.Owner)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public async Task<bool> DegradeManager(int userId, int organizationId)
        {
            try
            {
                var ouToDegrade = _projectClockDbContext.OrganizationsUsers.FirstOrDefault(ou =>
                    ou.OrganizationId == organizationId && ou.UserId == userId);

                if (ouToDegrade is null)
                {
                    return false;
                }
                else if (ouToDegrade.Role == Position.Owner || ouToDegrade.Role == Position.User)
                {
                    return false;
                }
                else
                {
                    ouToDegrade.Role = Position.User;
                    await _projectClockDbContext.SaveChangesAsync();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}