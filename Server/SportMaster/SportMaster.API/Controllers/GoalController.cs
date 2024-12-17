using AuthService.BLL.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportMaster.BLL.Dtos;
using SportMaster.BLL.Dtos.Request;
using SportMaster.BLL.Interfaces.Services;

namespace SportMaster.API.Controllers;

[Authorize]
[ApiController]
[Route("api/goals")]
public class GoalController : ControllerBase
{
    private readonly IGoalService _goalService;

    public GoalController(IGoalService goalService)
    {
        _goalService = goalService;
    }

    [HttpGet("user/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GoalDto>>> GetUserGoals(Guid userId, CancellationToken cancellationToken)
    {
        var result = await _goalService.GetUserGoalsWithProgressesAsync(userId, cancellationToken);
        return Ok(result);
    }

    [HttpGet("user/{userId}/without-progress")]
    public async Task<IActionResult> GetUserGoalsWithoutProgresses(Guid userId, CancellationToken cancellationToken = default)
    {
        var result = await _goalService.GetUserGoalsAsync(userId, cancellationToken);
        return Ok(result);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> AddGoal([FromBody] CreateGoalRequestDTO goalRequest,
        CancellationToken cancellationToken)
    {
        await _goalService.AddGoalAsync(goalRequest, cancellationToken);
        return Ok();
    }

    [HttpGet("user/{userId}/progress")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ProgressDto>> GetProgress(Guid userId, CancellationToken cancellationToken)
    {
        var result = await _goalService.GetProgressAsync(userId, cancellationToken);
        return Ok(result);
    }

    [HttpGet("user/{userId}/custom-goals")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CustomGoalDto>>> GetCustomGoals(Guid userId,
        CancellationToken cancellationToken)
    {
        var result = await _goalService.GetCustomGoalsAsync(userId, cancellationToken);
        return Ok(result);
    }

    [HttpPost("custom-goal")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> AddCustomGoal([FromBody] CreateCustomGoalRequestDTO customGoalRequest,
        CancellationToken cancellationToken)
    {
        await _goalService.AddCustomGoalAsync(customGoalRequest, cancellationToken);
        return Ok();
    }
}