using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportMaster.BLL.Dtos;
using SportMaster.BLL.Interfaces.Services;

namespace SportMaster.API.Controllers;

[Authorize]
[ApiController]
[Route("api/action-history")]
public class ActionHistoryController : ControllerBase
{
    private readonly IActionHistoryService _actionHistoryService;

    public ActionHistoryController(IActionHistoryService actionHistoryService)
    {
        _actionHistoryService = actionHistoryService;
    }

    [HttpGet("user/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ActionHistoryDto>>> GetUserActionHistory(Guid userId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
    {
        var result = await _actionHistoryService.GetUserActionHistoryAsync(userId, startDate, endDate);
        return Ok(result);
    }
}