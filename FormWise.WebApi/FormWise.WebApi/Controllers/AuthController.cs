using FormWise.WebApi.Models;
using FormWise.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormWise.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;


    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _authService.LoginAsync(request.Email, request.Password);

        return StatusCode(result.StatusCode, result);
    }
}