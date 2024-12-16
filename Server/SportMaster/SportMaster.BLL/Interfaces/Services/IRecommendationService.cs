using SportMaster.BLL.Dtos.Response;

namespace SportMaster.BLL.Interfaces.Services;

public interface IRecommendationService
{
    Task<RecommendationResponseDTO> GetRecommendationsAsync(Guid userId, CancellationToken cancellationToken = default);
}
