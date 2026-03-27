using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LoginAPI.Models;
using LoginAPI.Services;

namespace LoginAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthenticationService _authService;
    private readonly TokenService _tokenService;

    public AuthController(AuthenticationService authService, TokenService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterDto dto)
    {
        _authService.Register(dto);
        return Ok(new { message = "User registered successfully" });
    }
    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {
        var user = _authService.ValidateCredentials(dto);
        if (user == null) return Unauthorized(new { message = "Invalid Credentials" });

        var token = _tokenService.GenerateToken(user);
        Console.WriteLine("JWT generated: " + token);
        return Ok(new { token });
    }
    [Authorize]
    [HttpGet("secret")]
    public IActionResult GetSecret ()
    {
        return Ok("You accessed a protected route.");
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("admin")]
    public IActionResult AdminOnlyEndpoint()
    {
        return Ok("You accessed an Admin protected route.");
    }

}






