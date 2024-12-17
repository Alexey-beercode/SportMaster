using SportMaster.BLL.Dtos.Response;

namespace SportMaster.BLL.Interfaces.Services;

public interface IStepService
{
    Task AddStepLogAsync(Guid userId, int steps, CancellationToken cancellationToken);
    Task<IEnumerable<StepLogDTO>> GetStepLogsAsync(Guid userId, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken);
}
