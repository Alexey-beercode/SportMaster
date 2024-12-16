using Microsoft.EntityFrameworkCore;
using SportMaster.DAL.Config;
using SportMaster.DAL.Interfaces.Repositories;
using SportMaster.Domain.Entities;

namespace SportMaster.DAL.Repositories;

public class CustomGoalRepository : BaseRepository<CustomGoal>, ICustomGoalRepository
{
    public CustomGoalRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<IEnumerable<CustomGoal>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(cg => cg.UserId == userId && !cg.IsDeleted).ToListAsync(cancellationToken);
    }
}