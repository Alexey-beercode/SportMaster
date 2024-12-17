using SportMaster.BLL.Dtos;
using SportMaster.BLL.Dtos.Request;
using SportMaster.BLL.Dtos.Response;

namespace SportMaster.BLL.Interfaces.Services;

public interface IGoalService
{
    Task<IEnumerable<GoalWithProgressDTO>> GetUserGoalsWithProgressesAsync(Guid userId, CancellationToken cancellationToken = default);
    Task AddGoalAsync(CreateGoalRequestDTO goalRequest, CancellationToken cancellationToken = default);
    Task<ProgressDto> GetProgressAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<CustomGoalDto>> GetCustomGoalsAsync(Guid userId, CancellationToken cancellationToken = default);
    Task AddCustomGoalAsync(CreateCustomGoalRequestDTO customGoalRequest, CancellationToken cancellationToken = default);
    Task<IEnumerable<GoalDto>> GetUserGoalsAsync(Guid userId, CancellationToken cancellationToken = default);
}
