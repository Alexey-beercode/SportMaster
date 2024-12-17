using AutoMapper;
using SportMaster.BLL.Dtos;
using SportMaster.BLL.Dtos.Request;
using SportMaster.Domain.Entities;
using SportMaster.Domain.Enums;

namespace SportMaster.BLL.Mappers
{
    public class FoodLogProfile : Profile
    {
        public FoodLogProfile()
        {
            // Mapping FoodLog -> FoodLogDto
            CreateMap<FoodLog, FoodLogDto>()
                .ForMember(dest => dest.MealType, opt => opt.MapFrom(src => src.MealType.ToString()));

            // Mapping FoodLogRequestDTO -> FoodLog
            CreateMap<FoodLogRequestDTO, FoodLog>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID is auto-generated
                .ForMember(dest => dest.MealType, opt => opt.MapFrom(src => Enum.Parse<MealType>(src.MealType)));

            // Mapping FoodLogDto -> FoodLog (for updates if needed)
            CreateMap<FoodLogDto, FoodLog>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Prevent overwriting IDs
                .ForMember(dest => dest.MealType, opt => opt.MapFrom(src => Enum.Parse<MealType>(src.MealType)));
        }
    }
}