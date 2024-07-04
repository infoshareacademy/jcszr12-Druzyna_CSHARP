using ProjectClock.Database.Entities;

namespace ProjectClock.BusinessLogic.Services.OrganizationUserServices;

public interface IOrganizationUserService
{
    bool IsUserAnOwner(int userId);
    Task<IEnumerable<Organization>> GetUserOrganizations(int userId);
    Task<IEnumerable<User>> GetOrganizationUsers(int organizationId);
    Task<bool> IsUserSignedToOrganization(int userId, int organizationId);
    Task<IEnumerable<Organization>> GetUserAsAOwnerOrganization(int userId);

    Task<bool> RemoveUserFromOrganization(int userId, int organizationId);
}