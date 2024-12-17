using AutoMapper;
using SportMaster.BLL.Dtos;
using SportMaster.BLL.Dtos.Request;
using SportMaster.Domain.Entities;

namespace SportMaster.BLL.Mappers
{
    public class ProgressProfile : Profile
    {
        public ProgressProfile()
        {
            // Mapping Progress -> ProgressDto
            CreateMap<Progress, ProgressDto>();

            // Mapping ProgressRequestDTO -> Progress
            CreateMap<ProgressRequestDTO, Progress>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID is auto-generated
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date));

            // Mapping ProgressDto -> Progress (for updates if needed)
            CreateMap<ProgressDto, Progress>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Prevent overwriting IDs
        }
    }
}