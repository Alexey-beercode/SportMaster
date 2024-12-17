using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportMaster.BLL.Dtos.Response;
using SportMaster.BLL.Interfaces.Services;

namespace SportMaster.API.Controllers;

[Authorize]
[ApiController]
[Route("api/water")]
public class WaterController : ControllerBase
{
    private readonly IWaterService _waterService;

    public WaterController(IWaterService waterService)
    {
        _waterService = waterService;
    }

    [HttpPost("user/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> AddWaterLog(Guid userId, [FromBody] int glasses, CancellationToken cancellationToken)
    {
        await _waterService.AddWaterLogAsync(userId, glasses, cancellationToken);
        return Ok();
    }

    [HttpGet("user/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<WaterLogDTO>>> GetWaterLogs(Guid userId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, CancellationToken cancellationToken)
    {
        var result = await _waterService.GetWaterLogsAsync(userId, startDate, endDate, cancellationToken);
        return Ok(result);
    }
}