using Microsoft.EntityFrameworkCore;
using SportMaster.DAL.Config;
using SportMaster.DAL.Interfaces.Repositories;
using SportMaster.Domain.Entities;

namespace SportMaster.DAL.Repositories;

public class PersonalDataRepository : BaseRepository<PersonalData>, IPersonalDataRepository
{
    public PersonalDataRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<PersonalData> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(p => p.UserId == userId && !p.IsDeleted, cancellationToken);
    }
}