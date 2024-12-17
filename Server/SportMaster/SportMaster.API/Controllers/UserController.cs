using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportMaster.BLL.Dtos;
using SportMaster.BLL.Dtos.Request;
using SportMaster.BLL.Interfaces.Services;

namespace SportMaster.API.Controllers;

[Authorize]
[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<UserDto>> GetUserById(Guid userId, CancellationToken cancellationToken)
    {
        var result = await _userService.GetUserByIdAsync(userId, cancellationToken);
        return Ok(result);
    }

    [HttpPut("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> UpdateUser(Guid userId, [FromBody] UpdateUserRequestDTO updateUserRequest, CancellationToken cancellationToken)
    {
        await _userService.UpdateUserAsync(userId, updateUserRequest, cancellationToken);
        return Ok();
    }
}
