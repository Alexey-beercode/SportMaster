using Microsoft.EntityFrameworkCore;
using SportMaster.DAL.Config;
using SportMaster.DAL.Interfaces.Repositories;
using SportMaster.Domain.Entities;

namespace SportMaster.DAL.Repositories;

public class ExerciseLogRepository : BaseRepository<ExerciseLog>, IExerciseLogRepository
{
    public ExerciseLogRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<IEnumerable<ExerciseLog>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(e => e.UserId == userId && !e.IsDeleted).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ExerciseLog>> GetByDateRangeAsync(Guid userId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(e => e.UserId == userId && e.Date >= startDate && e.Date <= endDate && !e.IsDeleted).ToListAsync(cancellationToken);
    }
}