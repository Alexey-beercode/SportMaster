using AutoMapper;
using SportMaster.BLL.Dtos;
using SportMaster.BLL.Dtos.Request;
using SportMaster.Domain.Entities;

namespace SportMaster.BLL.Mappers
{
    public class ExerciseLogProfile : Profile
    {
        public ExerciseLogProfile()
        {
            // Mapping ExerciseLog -> ExerciseLogDto
            CreateMap<ExerciseLog, ExerciseLogDto>();

            // Mapping ExerciseLogRequestDTO -> ExerciseLog
            CreateMap<ExerciseLogRequestDTO, ExerciseLog>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID is auto-generated
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date));

            // Mapping ExerciseLogDto -> ExerciseLog (for updates if needed)
            CreateMap<ExerciseLogDto, ExerciseLog>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Prevent overwriting IDs
        }
    }
}