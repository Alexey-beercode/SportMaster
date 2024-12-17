using AutoMapper;
using SportMaster.BLL.Dtos;
using SportMaster.BLL.Dtos.Request;
using SportMaster.Domain.Entities;
using SportMaster.Domain.Enums;

namespace SportMaster.BLL.Mappers
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            // Mapping Notification -> NotificationDto
            CreateMap<Notification, NotificationDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));

            // Mapping NotificationRequestDTO -> Notification
            CreateMap<NotificationRequestDTO, Notification>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID is auto-generated
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<NotificationType>(src.Type)))
                .ForMember(dest => dest.IsRead, opt => opt.Ignore()); // IsRead defaults to false

            // Mapping NotificationDto -> Notification (for updates if needed)
            CreateMap<NotificationDto, Notification>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Prevent overwriting IDs
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<NotificationType>(src.Type)))
                .ForMember(dest => dest.IsRead, opt => opt.Ignore()); // Preserve existing IsRead state
        }
    }
}