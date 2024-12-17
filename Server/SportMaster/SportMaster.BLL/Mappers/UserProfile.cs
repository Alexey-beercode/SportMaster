using AutoMapper;
using SportMaster.BLL.Dtos;
using SportMaster.BLL.Dtos.Request;
using SportMaster.Domain.Entities;

namespace SportMaster.BLL.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        // Mapping User -> UserDto
        CreateMap<User, UserDto>();

        // Mapping UserRequestDTO -> User
        CreateMap<UserRequestDTO, User>();

        // Mapping UserDto -> User (for updates)
        CreateMap<UserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Prevent overwriting IDs
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

        CreateMap<UpdateUserRequestDTO, User>();
    }
}