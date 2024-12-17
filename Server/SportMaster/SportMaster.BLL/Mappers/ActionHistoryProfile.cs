using AutoMapper;
using SportMaster.BLL.Dtos;
using SportMaster.BLL.Dtos.Request;
using SportMaster.Domain.Entities;
using SportMaster.Domain.Enums;

namespace SportMaster.BLL.Mappers
{
    public class ActionHistoryProfile : Profile
    {
        public ActionHistoryProfile()
        {
            // Mapping ActionHistory -> ActionHistoryDto
            CreateMap<ActionHistory, ActionHistoryDto>()
                .ForMember(dest => dest.ActionType, opt => opt.MapFrom(src => src.ActionType.ToString()));

            // Mapping ActionHistoryRequestDTO -> ActionHistory
            CreateMap<ActionHistoryRequestDTO, ActionHistory>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID is auto-generated
                .ForMember(dest => dest.ActionType, opt => opt.MapFrom(src => Enum.Parse<ActionType>(src.ActionType)))
                .ForMember(dest => dest.ActionDate, opt => opt.MapFrom(src => src.ActionDate));

            // Mapping ActionHistoryDto -> ActionHistory (for updates if needed)
            CreateMap<ActionHistoryDto, ActionHistory>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Prevent overwriting IDs
                .ForMember(dest => dest.ActionType, opt => opt.MapFrom(src => Enum.Parse<ActionType>(src.ActionType)));
        }
    }
}