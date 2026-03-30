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
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        return await _authService.Register(dto);
        
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _authService.ValidateCredentials(dto);
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






