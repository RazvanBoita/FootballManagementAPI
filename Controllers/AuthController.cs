using FootballMgm.Api.Data;
using FootballMgm.Api.Dtos;
using FootballMgm.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FootballMgm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly FootballDbContext _dbContext;
        private readonly AuthService _authService;

        public AuthController(IConfiguration configuration, FootballDbContext dbContext, AuthService authService)
        {
            _configuration = configuration;
            _dbContext = dbContext;
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Auth([FromBody] AuthDto authModel)
        {
            var result = await _authService.Signup(authModel);
            if (result.Sucess)
            {
                return Ok(result.Message);
            }
        
            return Unauthorized(result.Message);
        }
    }
}
