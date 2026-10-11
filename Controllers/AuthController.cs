using Microsoft.AspNetCore.Mvc;
using ProductStoreApi.Models;
using ProductStoreApi.Services;

namespace ProductStoreApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private static readonly Dictionary<string, string> Users = new()
    {
        ["student"] = "12345",
        ["manager"] = "manager2026"
    };

    private readonly JwtTokenService _tokenService;

    public AuthController(JwtTokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<LoginResponse> Login(LoginRequest request)
    {
        if (!Users.TryGetValue(request.Username, out var password) || password != request.Password)
            return Unauthorized(new { message = "Неверный логин или пароль" });

        return Ok(_tokenService.Generate(request.Username));
    }
}
