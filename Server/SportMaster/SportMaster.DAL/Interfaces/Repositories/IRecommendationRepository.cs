using SportMaster.Domain.Entities;

namespace SportMaster.DAL.Interfaces.Repositories;

public interface IRecommendationRepository : IBaseRepository<Recommendation>
{
    Task<IEnumerable<Recommendation>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}