using SportMaster.Domain.Entities;

namespace SportMaster.DAL.Interfaces.Repositories;

public interface ICustomGoalRepository : IBaseRepository<CustomGoal>
{
    Task<IEnumerable<CustomGoal>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}