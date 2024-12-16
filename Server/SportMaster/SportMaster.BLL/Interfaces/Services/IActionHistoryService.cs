using SportMaster.BLL.Dtos;

namespace SportMaster.BLL.Interfaces.Services;

public interface IActionHistoryService
{
    Task<IEnumerable<ActionHistoryDto>> GetUserActionHistoryAsync(Guid userId, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken = default);
}
