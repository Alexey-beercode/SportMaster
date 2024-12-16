using Microsoft.EntityFrameworkCore;
using SportMaster.DAL.Config;
using SportMaster.DAL.Interfaces.Repositories;
using SportMaster.Domain.Entities;

namespace SportMaster.DAL.Repositories;

public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
{
    public NotificationRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<IEnumerable<Notification>> GetUnreadNotificationsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(n => n.UserId == userId && !n.IsDeleted && !n.IsRead).ToListAsync(cancellationToken);
    }
}