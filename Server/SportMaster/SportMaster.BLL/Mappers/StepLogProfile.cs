using AutoMapper;
using SportMaster.BLL.Dtos.Response;
using SportMaster.Domain.Entities;

namespace SportMaster.BLL.Mappers;

public class StepLogProfile : Profile
{
    public StepLogProfile()
    {
        // Mapping StepLog -> StepLogDTO
        CreateMap<StepLog, StepLogDTO>();

        // Mapping StepLogDTO -> StepLog (if needed)
        CreateMap<StepLogDTO, StepLog>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID is auto-generated
            .ForMember(dest => dest.UserId, opt => opt.Ignore()); // UserId is typically handled in the service layer
    }
}