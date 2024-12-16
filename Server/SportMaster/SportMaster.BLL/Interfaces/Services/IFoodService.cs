using SportMaster.BLL.Dtos;
using SportMaster.BLL.Dtos.Request;

namespace SportMaster.BLL.Interfaces.Services;

public interface IFoodService
{
    Task<IEnumerable<FoodLogDto>> GetFoodLogsAsync(Guid userId, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken = default);
    Task AddFoodLogAsync(FoodLogRequestDTO foodLogRequest, CancellationToken cancellationToken = default);
}
