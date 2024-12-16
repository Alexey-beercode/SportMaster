using SportMaster.BLL.Dtos;
using SportMaster.BLL.Dtos.Request;

namespace SportMaster.BLL.Interfaces.Services;

public interface IUserService
{
    Task<UserDto> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task UpdateUserAsync(Guid userId, UpdateUserRequestDTO updateUserRequest, CancellationToken cancellationToken = default);
}
