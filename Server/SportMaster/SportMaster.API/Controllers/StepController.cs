using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportMaster.BLL.Dtos.Response;
using SportMaster.BLL.Interfaces.Services;

namespace SportMaster.API.Controllers;

[Authorize]
[ApiController]
[Route("api/steps")]
public class StepController : ControllerBase
{
    private readonly IStepService _stepService;

    public StepController(IStepService stepService)
    {
        _stepService = stepService;
    }

    [HttpPost("user/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> AddStepLog(Guid userId, [FromBody] int steps, CancellationToken cancellationToken)
    {
        await _stepService.AddStepLogAsync(userId, steps, cancellationToken);
        return Ok();
    }

    [HttpGet("user/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<StepLogDTO>>> GetStepLogs(Guid userId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, CancellationToken cancellationToken)
    {
        var result = await _stepService.GetStepLogsAsync(userId, startDate, endDate, cancellationToken);
        return Ok(result);
    }
}