using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportMaster.BLL.Dtos;
using SportMaster.BLL.Dtos.Request;
using SportMaster.BLL.Interfaces.Services;

namespace SportMaster.API.Controllers;

[Authorize]
[ApiController]
[Route("api/exercises")]
public class ExerciseController : ControllerBase
{
    private readonly IExerciseService _exerciseService;

    public ExerciseController(IExerciseService exerciseService)
    {
        _exerciseService = exerciseService;
    }

    [HttpGet("user/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ExerciseLogDto>>> GetExerciseLogs(Guid userId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
    {
        var result = await _exerciseService.GetExerciseLogsAsync(userId, startDate, endDate);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> AddExerciseLog([FromBody] ExerciseLogRequestDTO exerciseLogRequest, CancellationToken cancellationToken)
    {
        await _exerciseService.AddExerciseLogAsync(exerciseLogRequest, cancellationToken);
        return Ok();
    }
}