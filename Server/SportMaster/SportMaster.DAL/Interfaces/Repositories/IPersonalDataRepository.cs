using SportMaster.Domain.Entities;

namespace SportMaster.DAL.Interfaces.Repositories;

public interface IPersonalDataRepository : IBaseRepository<PersonalData>
{
    Task<PersonalData> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}