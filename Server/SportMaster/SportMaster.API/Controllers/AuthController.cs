using AuthService.BLL.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportMaster.BLL.Dtos.Request;
using SportMaster.BLL.Dtos.Request.Auth;
using SportMaster.BLL.Dtos.Response;

namespace SportMaster.API.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<AuthReponseDTO>> Register([FromBody] UserRequestDTO registerDto, CancellationToken cancellationToken)
    {
        var result = await _authService.RegisterAsync(registerDto, cancellationToken);
        return Ok(result);
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<AuthReponseDTO>> Login([FromBody] LoginDTO loginDto, CancellationToken cancellationToken)
    {
        var result = await _authService.LoginAsync(loginDto, cancellationToken);
        return Ok(result);
    }
    [Authorize]
    [HttpGet("token_status")]
    public async Task<IActionResult> GetTokenStatusAsync()
    {
        return Ok("Token is valid");
    }
}