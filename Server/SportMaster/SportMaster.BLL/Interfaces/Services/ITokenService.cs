using SportMaster.Domain.Entities;

namespace SportMaster.BLL.Interfaces.Services;

public interface ITokenService
{
    string GenerateAccessToken(User user);
}