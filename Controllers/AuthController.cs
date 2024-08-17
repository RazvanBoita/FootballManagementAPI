using FluentValidation;
using FootballMgm.Api.Data;
using FootballMgm.Api.Dtos;
using FootballMgm.Api.Services;
using FootballMgm.Api.Validators.FootballValidators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FootballMgm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IValidator<AuthDto> _authValidator;
        
        public AuthController(AuthService authService, IValidator<AuthDto> authValidator)
        {
            _authService = authService;
            _authValidator = authValidator;
        }

        [HttpPost]
        public async Task<IActionResult> Auth([FromBody] AuthDto authModel)
        {
            var validationResult = _authValidator.Validate(authModel);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            
            var result = await _authService.Signup(authModel);
            if (result.Sucess)
            {
                return Ok(result.Message);
            }
        
            return Unauthorized(result.Message);
        }
    }
}
