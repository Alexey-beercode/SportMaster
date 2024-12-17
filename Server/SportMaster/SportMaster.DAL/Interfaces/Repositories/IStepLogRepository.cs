using SportMaster.Domain.Entities;

namespace SportMaster.DAL.Interfaces.Repositories;

public interface IStepLogRepository : IBaseRepository<StepLog>
{
    Task<IEnumerable<StepLog>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}