using Microsoft.EntityFrameworkCore;
using SportMaster.DAL.Config;
using SportMaster.DAL.Interfaces.Repositories;
using SportMaster.Domain.Entities;

namespace SportMaster.DAL.Repositories;

public class ProgressRepository : BaseRepository<Progress>, IProgressRepository
{
    public ProgressRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<Progress> GetLatestProgressByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(p => p.UserId == userId && !p.IsDeleted).OrderByDescending(p => p.Date).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Progress>> GetProgressHistoryByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(p => p.UserId == userId && !p.IsDeleted).OrderBy(p => p.Date).ToListAsync(cancellationToken);
    }
}