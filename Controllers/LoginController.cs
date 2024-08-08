using FootballMgm.Api.Data;
using FootballMgm.Api.Dtos;
using FootballMgm.Api.Services;
using FootballMgm.Api.Utils;

namespace FootballMgm.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly FootballDbContext _dbContext;
    private readonly AuthService _authService;

    public LoginController(IConfiguration configuration, FootballDbContext dbContext, AuthService authService)
    {
        _configuration = configuration;
        _dbContext = dbContext;
        _authService = authService;
    }

    [HttpPost]
    public IActionResult Login([FromBody] AuthDto loginDto)
    {
        var result = _authService.Login(loginDto);
        if (result.Success)
        {
            return Ok(new{result.Message, result.jwt});
        }

        return Unauthorized(result.Message);
    }
}
