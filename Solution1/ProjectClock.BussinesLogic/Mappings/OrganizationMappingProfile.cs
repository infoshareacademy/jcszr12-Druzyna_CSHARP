using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProjectClock.BusinessLogic.Dtos.OrganizationDto;
using ProjectClock.Database.Entities;

namespace ProjectClock.BusinessLogic.Mappings
{
    public class OrganizationMappingProfile : Profile
    {
        public OrganizationMappingProfile()
        {
            CreateMap<OrganizationDto, Organization>();
        }
    }
}
