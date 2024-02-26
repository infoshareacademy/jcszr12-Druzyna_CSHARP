using ProjectClock.Database;
using ProjectClock.Database.Entities;

namespace ProjectClock.BusinessLogic.Services
{
    public class UserServices : IUserServices
    {
        private ProjectClockDbContext _projectClockDbContext;
        public UserServices(ProjectClockDbContext projectClockDbContext)
        {
            _projectClockDbContext = projectClockDbContext;
        }

        public bool Create(User user)
        {
            try
            {
                if (UserExist(user.Id))
                {
                    throw new Exception($"This user already exist");
                    return false;
                }
                else
                {
                    _projectClockDbContext.Users.Add(user);
                    _projectClockDbContext.SaveChanges();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public User? GetById(int id)
        {
            return _projectClockDbContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public List<User> GetAll()
        {
            return _projectClockDbContext.Users.ToList();
        }

        public void Update(User model)
        {
            var user = GetById(model.Id);

            user.Name = model.Name;
            user.Email = model.Email;
            user.UserPosition = model.UserPosition;
            user.Surname = model.Surname;

        }

        public bool Delete(int id)
        {
            try
            {
                if (!UserExist(id))
                {
                    throw new Exception($"This user doesn't exist");
                    return false;
                }
                else
                {
                    var user = GetById(id);
                    _projectClockDbContext.Users.Remove(user);
                    _projectClockDbContext.SaveChanges();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool UserExist(int id)
        {
            return _projectClockDbContext.Users.Any(u => u.Id == id);
        }
    }

    public interface IUserServices
    {
        bool Create(User user);
        User? GetById(int id);
        List<User> GetAll();
        void Update(User model);
        bool Delete(int id);
        bool UserExist(int id);

    }
}
