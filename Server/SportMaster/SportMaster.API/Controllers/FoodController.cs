using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportMaster.BLL.Dtos;
using SportMaster.BLL.Dtos.Request;
using SportMaster.BLL.Interfaces.Services;

namespace SportMaster.API.Controllers;

[Authorize]
[ApiController]
[Route("api/food")]
public class FoodController : ControllerBase
{
    private readonly IFoodService _foodService;

    public FoodController(IFoodService foodService)
    {
        _foodService = foodService;
    }

    [HttpGet("user/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<FoodLogDto>>> GetFoodLogs(Guid userId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
    {
        var result = await _foodService.GetFoodLogsAsync(userId, startDate, endDate);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> AddFoodLog([FromBody] FoodLogRequestDTO foodLogRequest, CancellationToken cancellationToken)
    {
        await _foodService.AddFoodLogAsync(foodLogRequest, cancellationToken);
        return Ok();
    }
}