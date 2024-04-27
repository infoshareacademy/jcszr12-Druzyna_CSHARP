using ProjectClock.Database;

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
    }

    public interface IOrganizationUserServices
    {
        bool IsUserAnOwner(int userId);
    }
}