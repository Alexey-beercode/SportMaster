using SportMaster.Domain.Entities;

namespace SportMaster.DAL.Interfaces.Repositories;

public interface IProgressRepository : IBaseRepository<Progress>
{
    Task<Progress> GetLatestProgressByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Progress>> GetProgressHistoryByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}