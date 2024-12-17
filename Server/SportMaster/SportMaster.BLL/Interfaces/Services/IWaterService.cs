using SportMaster.BLL.Dtos.Response;

namespace SportMaster.BLL.Interfaces.Services;

public interface IWaterService
{
    Task AddWaterLogAsync(Guid userId, int glasses, CancellationToken cancellationToken);
    Task<IEnumerable<WaterLogDTO>> GetWaterLogsAsync(Guid userId, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken);
}
