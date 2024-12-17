using SportMaster.Domain.Entities;

namespace SportMaster.DAL.Interfaces.Repositories;

public interface IWaterLogRepository : IBaseRepository<WaterLog>
{
    Task<IEnumerable<WaterLog>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}