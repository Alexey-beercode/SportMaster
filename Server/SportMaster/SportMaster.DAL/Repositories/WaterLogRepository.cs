using Microsoft.EntityFrameworkCore;
using SportMaster.DAL.Config;
using SportMaster.DAL.Interfaces.Repositories;
using SportMaster.Domain.Entities;

namespace SportMaster.DAL.Repositories;

public class WaterLogRepository : BaseRepository<WaterLog>, IWaterLogRepository
{
    public WaterLogRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    
    public async Task<IEnumerable<WaterLog>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(log => log.UserId == userId && !log.IsDeleted).ToListAsync(cancellationToken);
    }
}
