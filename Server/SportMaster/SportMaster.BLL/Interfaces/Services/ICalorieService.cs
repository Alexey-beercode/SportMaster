using SportMaster.BLL.Dtos.Response;

namespace SportMaster.BLL.Interfaces.Services;

public interface ICalorieService
{
    Task<UserCaloriesDTO> CalculateDailyCaloriesAsync(Guid userId, CancellationToken cancellationToken);
}
