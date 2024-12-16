using SportMaster.BLL.Dtos;
using SportMaster.BLL.Dtos.Request;

namespace SportMaster.BLL.Interfaces.Services;

public interface IGoalService
{
    Task<IEnumerable<GoalDto>> GetUserGoalsAsync(Guid userId, CancellationToken cancellationToken = default);
    Task AddGoalAsync(CreateGoalRequestDTO goalRequest, CancellationToken cancellationToken = default);
    Task<ProgressDto> GetProgressAsync(Guid userId, CancellationToken cancellationToken = default);
}
