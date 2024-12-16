using Microsoft.EntityFrameworkCore;
using SportMaster.DAL.Config;
using SportMaster.DAL.Interfaces.Repositories;
using SportMaster.Domain.Entities;

namespace SportMaster.DAL.Repositories;

public class FoodLogRepository : BaseRepository<FoodLog>, IFoodLogRepository
{
    public FoodLogRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<IEnumerable<FoodLog>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(f => f.UserId == userId && !f.IsDeleted).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<FoodLog>> GetByDateRangeAsync(Guid userId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(f => f.UserId == userId && f.Date >= startDate && f.Date <= endDate && !f.IsDeleted).ToListAsync(cancellationToken);
    }
}