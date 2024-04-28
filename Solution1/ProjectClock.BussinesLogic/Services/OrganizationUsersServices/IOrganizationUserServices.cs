namespace ProjectClock.BusinessLogic.Services.OrganizationUserServices;

public interface IOrganizationUserServices
{
    bool IsUserAnOwner(int userId);
}