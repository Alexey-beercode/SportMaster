using Microsoft.EntityFrameworkCore;
using SportMaster.DAL.Config;
using SportMaster.DAL.Interfaces.Repositories;
using SportMaster.Domain.Entities;

namespace SportMaster.DAL.Repositories;

public class GoalRepository : BaseRepository<Goal>, IGoalRepository
{
    public GoalRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<IEnumerable<Goal>> GetActiveGoalsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(g => g.UserId == userId && !g.IsDeleted).ToListAsync(cancellationToken);
    }
}