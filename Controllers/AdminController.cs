using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballMgm.Api.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Policy = "RequireAdminRole")]
public class AdminController : ControllerBase
{
    [HttpGet]
    [Route("hi")]
    public IActionResult SayHi()
    {
        return Ok("Hello admin!");
    }
    
    
}