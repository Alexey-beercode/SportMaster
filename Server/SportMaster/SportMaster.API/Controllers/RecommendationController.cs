using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportMaster.BLL.Dtos.Response;
using SportMaster.BLL.Interfaces.Services;

namespace SportMaster.API.Controllers;
[Authorize]
[ApiController]
[Route("api/recommendations")]
public class RecommendationController : ControllerBase
{
    private readonly IRecommendationService _recommendationService;

    public RecommendationController(IRecommendationService recommendationService)
    {
        _recommendationService = recommendationService;
    }

    [HttpGet("user/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<RecommendationResponseDTO>> GetRecommendations(Guid userId, CancellationToken cancellationToken)
    {
        var result = await _recommendationService.GetRecommendationsAsync(userId, cancellationToken);
        return Ok(result);
    }
}