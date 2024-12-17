using AutoMapper;
using SportMaster.BLL.Dtos;
using SportMaster.BLL.Dtos.Request;
using SportMaster.Domain.Entities;
using SportMaster.Domain.Enums;

namespace SportMaster.BLL.Mappers
{
    public class GoalProfile : Profile
    {
        public GoalProfile()
        {
            // Mapping Goal -> GoalDto
            CreateMap<Goal, GoalDto>()
                .ForMember(dest => dest.GoalType, opt => opt.MapFrom(src => src.GoalType.ToString()));

            // Mapping GoalRequestDTO -> Goal
            CreateMap<GoalRequestDTO, Goal>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID is auto-generated
                .ForMember(dest => dest.GoalType, opt => opt.MapFrom(src => Enum.Parse<GoalType>(src.GoalType)))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            // Mapping GoalDto -> Goal (for updates if needed)
            CreateMap<GoalDto, Goal>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Prevent overwriting IDs
                .ForMember(dest => dest.GoalType, opt => opt.MapFrom(src => Enum.Parse<GoalType>(src.GoalType)))
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()); // CreatedDate should not be updated
            CreateMap<CreateGoalRequestDTO, Goal>()
                .ForMember(dest => dest.GoalType, opt => opt.MapFrom(src => Enum.Parse<GoalType>(src.GoalType)));
        }
    }
}