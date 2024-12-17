using AutoMapper;
using SportMaster.BLL.Dtos.Response;
using SportMaster.Domain.Entities;

namespace SportMaster.BLL.Mappers;

public class WaterLogProfile : Profile
{
    public WaterLogProfile()
    {
        // Mapping WaterLog -> WaterLogDTO
        CreateMap<WaterLog, WaterLogDTO>();

        // Mapping WaterLogDTO -> WaterLog (if needed)
        CreateMap<WaterLogDTO, WaterLog>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID is auto-generated
            .ForMember(dest => dest.UserId, opt => opt.Ignore()); // UserId is typically handled in the service layer
    }
}