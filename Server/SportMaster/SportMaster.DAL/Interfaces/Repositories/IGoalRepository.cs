using SportMaster.Domain.Entities;

namespace SportMaster.DAL.Interfaces.Repositories;

public interface IGoalRepository : IBaseRepository<Goal>
{
    Task<IEnumerable<Goal>> GetActiveGoalsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}