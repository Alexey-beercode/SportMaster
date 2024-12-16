using SportMaster.BLL.Dtos;
using SportMaster.BLL.Dtos.Request;

namespace SportMaster.BLL.Interfaces.Services;

public interface IExerciseService
{
    Task<IEnumerable<ExerciseLogDto>> GetExerciseLogsAsync(Guid userId, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken = default);
    Task AddExerciseLogAsync(ExerciseLogRequestDTO exerciseLogRequest, CancellationToken cancellationToken = default);
}
