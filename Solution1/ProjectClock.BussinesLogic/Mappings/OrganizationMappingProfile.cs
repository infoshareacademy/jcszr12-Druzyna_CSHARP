using AutoMapper;
using ProjectClock.BusinessLogic.Dtos.Organization;
using ProjectClock.Database.Entities;

namespace ProjectClock.BusinessLogic.Mappings
{
    public class OrganizationMappingProfile : Profile
    {
        public OrganizationMappingProfile()
        {
            CreateMap<CreateOrganizationDto, Organization>();
        }
    }
}