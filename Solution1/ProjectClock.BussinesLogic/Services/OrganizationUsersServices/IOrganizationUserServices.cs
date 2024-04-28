using ProjectClock.Database.Entities;

namespace ProjectClock.BusinessLogic.Services.OrganizationUserServices;

public interface IOrganizationUserServices
{
    bool IsUserAnOwner(int userId);
    Task<IEnumerable<Organization>> GetUserOrganizations(int userId);
}