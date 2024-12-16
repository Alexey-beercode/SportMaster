using SportMaster.BLL.Dtos.Request;
using SportMaster.BLL.Dtos.Request.Auth;
using SportMaster.BLL.Dtos.Response;

namespace AuthService.BLL.Interfaces.Services;

public interface IAuthService
{
    Task<AuthReponseDTO> RegisterAsync(UserRequestDTO registerDto,CancellationToken cancellationToken=default);
    Task<AuthReponseDTO> LoginAsync(LoginDTO loginDto,CancellationToken cancellationToken=default);
}