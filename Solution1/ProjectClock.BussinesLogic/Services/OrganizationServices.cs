using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectClock.BusinessLogic.Dtos.Organization;
using ProjectClock.Database;
using ProjectClock.Database.Entities;

namespace ProjectClock.BusinessLogic.Services
{
    public interface IOrganizationServices
    {
        Task<bool> Create(CreateOrganizationDto organization);
        Task<Organization> GetById(int id);
        Task<List<Organization>> GetAll();
        Task Update(Organization model);
        Task<bool> Delete(int id);
        Task<bool> OrganizationExist(string name);

        Task<bool> AddUser(int organizationId, int userId);
    }

    public class OrganizationServices : IOrganizationServices
    {
        private ProjectClockDbContext _projectClockDbContext;
        private IMapper _mapper;

        public OrganizationServices(ProjectClockDbContext projectClockDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _projectClockDbContext = projectClockDbContext;
        }

        public OrganizationServices(ProjectClockDbContext projectClockDbContext)
        {
            _projectClockDbContext = projectClockDbContext;
        }
        public async Task<bool> Create(Organization organization)
        {
            try
            {
                if (await OrganizationExist(organization.Name))
                {
                    return false;
                }
                else
                {
                    await _projectClockDbContext.Organizations.AddAsync(organization);
                    await _projectClockDbContext.SaveChangesAsync();
                    return true;

                }

            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> Create(CreateOrganizationDto organizationDto)
        {
            var organization = _mapper.Map<Organization>(organizationDto);

            try
            {
                if (await OrganizationExist(organization.Name))
                {
                    return false;
                }
                else
                {
                    await _projectClockDbContext.Organizations.AddAsync(organization);
                    await _projectClockDbContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<Organization> GetById(int id)
        {
            return await _projectClockDbContext.Organizations.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<Organization>> GetAll()
        {
            return await _projectClockDbContext.Organizations.Include(o => o.Projects).Include(o=>o.OrganizationUsers).ThenInclude(u=>u.User).ToListAsync();
        }

        public async Task Update(Organization model)
        {
            var organization = await GetById(model.Id);

            organization.Name = model.Name;

            await _projectClockDbContext.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var organization = await GetById(id);

                if (organization is null)
                {
                    return false;
                }
                else
                {

                    _projectClockDbContext.Organizations.Remove(organization);
                    await _projectClockDbContext.SaveChangesAsync();
                    return true;
                }

            }
            catch (Exception)
            { 
                return false;
            }

        }

        public async Task<bool> OrganizationExist(string name)
        {
            return await _projectClockDbContext.Organizations.AsNoTracking().AnyAsync(u => u.Name == name);
        }

        public async Task<bool> AddUser(int organizationId, int userId)
        {
            try
            {
                var user = await _projectClockDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
                var organization = await GetById(organizationId);

                if (user is null || organization is null)
                {
                    return false;
                }
                else
                {
                    OrganizationUser organizationUser = new OrganizationUser() { User = user, Organization = organization };
                    await _projectClockDbContext.OrganizationsUsers.AddAsync(organizationUser);
                    await _projectClockDbContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> AddProjectToOrganization(int organizationId, Project project)
        {
            
            var organization = await _projectClockDbContext.Organizations.FindAsync(organizationId);

            if (organization == null)
            {
                return false;
            }

            
            organization.Projects.Add(project);
            await _projectClockDbContext.SaveChangesAsync();

            return true;
        }
    }
}
