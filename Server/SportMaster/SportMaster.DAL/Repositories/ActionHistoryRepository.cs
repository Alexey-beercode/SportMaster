using Microsoft.EntityFrameworkCore;
using SportMaster.DAL.Config;
using SportMaster.DAL.Interfaces.Repositories;
using SportMaster.Domain.Entities;

namespace SportMaster.DAL.Repositories;

public class ActionHistoryRepository : BaseRepository<ActionHistory>, IActionHistoryRepository
{
    public ActionHistoryRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<IEnumerable<ActionHistory>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(a => a.UserId == userId && !a.IsDeleted).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ActionHistory>> GetByDateRangeAsync(Guid userId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(a => a.UserId == userId && a.ActionDate >= startDate && a.ActionDate <= endDate && !a.IsDeleted).ToListAsync(cancellationToken);
    }
}