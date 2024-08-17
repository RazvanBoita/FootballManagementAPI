using FluentValidation;
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
    private readonly IValidator<AuthDto> _authValidator;
    private readonly AuthService _authService;

    public LoginController(AuthService authService, IValidator<AuthDto> authValidator)
    {
        _authService = authService;
        _authValidator = authValidator; 
    }

    [HttpPost]
    public IActionResult Login([FromBody] AuthDto loginDto)
    {
        var validationResult = _authValidator.Validate(loginDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        
        var result = _authService.Login(loginDto);
        if (result.Success)
        {
            return Ok(new{result.Message, result.jwt});
        }

        return Unauthorized(result.Message);
    }
}
