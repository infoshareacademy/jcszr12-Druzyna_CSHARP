using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> Create(User user)
        {
            try
            {
                if (await UserExist(user.Email))
                {
                    throw new Exception($"This user already exist");
                    
                }
                else
                {
                    _projectClockDbContext.Users.Add(user);
                    await _projectClockDbContext.SaveChangesAsync();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<User?> GetById(int id)
        {
            return await _projectClockDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<User>> GetAll()
        {
            return await _projectClockDbContext.Users.ToListAsync();
        }

        public async Task Update(User model)
        {
            var user = await GetById(model.Id);

            user.Name = model.Name;
            user.Email = model.Email;
            user.Surname = model.Surname;

            await _projectClockDbContext.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var user = await GetById(id);

                if (user is null)
                {
                    throw new Exception($"This user doesn't exist");
                    return false;
                }
                else
                {
                    
                    _projectClockDbContext.Users.Remove(user);
                    await _projectClockDbContext.SaveChangesAsync();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> UserExist(string email)
        {
            return await _projectClockDbContext.Users.AsNoTracking().AnyAsync(u => u.Email == email);
        }
    }

    public interface IUserServices
    {
        Task<bool> Create(User user);
        Task<User> GetById(int id);
        Task<List<User>> GetAll();
        Task Update(User model);
        Task<bool> Delete(int id);
        Task<bool> UserExist(string email);

    }
}
