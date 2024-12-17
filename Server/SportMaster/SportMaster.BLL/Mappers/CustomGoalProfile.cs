using AutoMapper;
using SportMaster.BLL.Dtos;
using SportMaster.BLL.Dtos.Request;
using SportMaster.Domain.Entities;

namespace SportMaster.BLL.Mappers;

public class CustomGoalProfile : Profile
{
    public CustomGoalProfile()
    {
        // Mapping CustomGoal -> CustomGoalDto
        CreateMap<CustomGoal, CustomGoalDto>();

        // Mapping CreateCustomGoalRequestDTO -> CustomGoal
        CreateMap<CreateCustomGoalRequestDTO, CustomGoal>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID is auto-generated
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

        // Mapping CustomGoalDto -> CustomGoal (for updates if needed)
        CreateMap<CustomGoalDto, CustomGoal>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Prevent overwriting IDs
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()); // CreatedDate should not be updated
    }
}