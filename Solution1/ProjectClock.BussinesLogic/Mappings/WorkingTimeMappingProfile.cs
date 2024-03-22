using AutoMapper;
using ProjectClock.BusinessLogic.Dtos.WorkingTime.WorkingTimeDtos;
using ProjectClock.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClock.BusinessLogic.Mapping
{
    public class WorkingTimeMappingProfile : Profile
    {
        public WorkingTimeMappingProfile()
        {
            CreateMap<WorkingTime, NotFinisedWorkingTimeDto>()
            .ForMember(dest => dest.WorkingTimeId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.Name))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
            .ForMember(dest => dest.IsFinished, opt => opt.MapFrom(src => src.IsFinished));
        
        }
    }
}
