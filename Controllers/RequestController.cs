using System.Runtime.ExceptionServices;
using FootballMgm.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FootballMgm.Api.Controllers;

[ApiController]
[Route("api/requests")]
[Authorize(Policy = "RequireAdminRole")]
public class RequestController : ControllerBase
{
    private readonly IAdminService _adminService;
    private readonly IPromotionService _promotionService;
    public RequestController(IAdminService adminService, IPromotionService promotionService)
    {
        _adminService = adminService;
        _promotionService = promotionService;
    }
    

    [HttpGet]
    [Route("footballer")]
    public IActionResult GetAllFootballerRequests()
    {
        return Ok(_adminService.GetAllFootballerRequests());
    }

    [HttpGet("footballer/{userId:int}")]
    public IActionResult GetFootballerRequestById(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest("Invalid ID");
        }
        
        return Ok(_adminService.GetFootballerRequestById(userId));
    }

    [HttpGet("footballer/{username}")]
    public IActionResult GetFootballerRequestByUsername(string username)
    {
        return Ok(_adminService.GetFootballerRequestByUsername(username));
    }

    [HttpDelete("footballer/{id:int}")]
    public IActionResult DeleteFootballerRequest(int id)
    {
        var result = _adminService.DeleteFootballerRequestById(id);
        if (result)
        {
            return Ok($"Deleted footballer with id {id}.");
        }

        return NotFound($"No footballer with id {id} found.");
    }
    
    [HttpDelete("footballer/{username}")]
    public IActionResult DeleteFootballerRequest(string username)
    {
        var result = _adminService.DeleteFootballerRequestByUsername(username);
        if (result)
        {
            return Ok($"Deleted footballer with name {username}.");
        }

        return NotFound($"No footballer with name {username} found.");
    }

    [HttpPut("footballer/{userId:int}")]
    public IActionResult PromoteUserToFootballer(int userId)
    {
        var (result, message) = _promotionService.PerformFootballerPromotion(_adminService.GetFootballerRequestById(userId));
        if (!result)
        {
            return BadRequest(message);
        }

        return Ok(message);
    }
}