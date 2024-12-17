using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportMaster.BLL.Dtos.Response;
using SportMaster.BLL.Interfaces.Services;

namespace SportMaster.API.Controllers;

[Authorize]
[ApiController]
[Route("api/calories")]
public class CalorieController : ControllerBase
{
    private readonly ICalorieService _calorieService;

    public CalorieController(ICalorieService calorieService)
    {
        _calorieService = calorieService;
    }

    [HttpGet("user/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<UserCaloriesDTO>> CalculateDailyCalories(Guid userId,CancellationToken cancellationToken=default)
    {
        var result = await _calorieService.CalculateDailyCaloriesAsync(userId, cancellationToken);
        return Ok(result);
    }
}