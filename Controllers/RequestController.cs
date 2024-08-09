using System.Runtime.ExceptionServices;
using System.Security.Claims;
using FootballMgm.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FootballMgm.Api.Controllers;

[ApiController]
[Route("api/requests")]
public class RequestController : ControllerBase
{
    private readonly IRequestService _requestService;
    private readonly IPromotionService _promotionService;
    public RequestController(IRequestService requestService, IPromotionService promotionService)
    {
        _requestService = requestService;
        _promotionService = promotionService;
    }
    
    
    //TODO START FOOTBALLERS
    [Authorize(Policy = "RequireAdminRole")]
    [HttpGet]
    [Route("footballers")]
    public IActionResult GetAllFootballerRequests()
    {
        return Ok(_requestService.GetAllFootballerRequests());
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpGet("footballers/{userId:int}")]
    public IActionResult GetFootballerRequestById(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest("Invalid ID");
        }
        
        return Ok(_requestService.GetFootballerRequestById(userId));
    }

    
    [Authorize(Policy = "RequireAdminRole")]
    [HttpGet("footballers/{username}")]
    public IActionResult GetFootballerRequestByUsername(string username)
    {
        return Ok(_requestService.GetFootballerRequestByUsername(username));
    }
    
    
    [Authorize(Policy = "RequireAdminRole")]
    [HttpDelete("footballers/{id:int}")]
    public IActionResult DeleteFootballerRequest(int id)
    {
        var result = _requestService.DeleteFootballerRequestById(id);
        if (result)
        {
            return Ok($"Deleted footballer with id {id}.");
        }

        return NotFound($"No footballer with id {id} found.");
    }
    
    [Authorize(Policy = "RequireAdminRole")]
    [HttpDelete("footballers/{username}")]
    public IActionResult DeleteFootballerRequest(string username)
    {
        var result = _requestService.DeleteFootballerRequestByUsername(username);
        if (result)
        {
            return Ok($"Deleted footballer with name {username}.");
        }

        return NotFound($"No footballer with name {username} found.");
    }

    [Authorize(Policy = "RequireAdminOrCoachRole")]
    [HttpPut("footballers/{userId:int}")]
    public IActionResult PromoteUserToFootballer(int userId)
    {
        if (_requestService.GetFootballerRequestById(userId) is null)
        {
            return BadRequest($"No footballer request found for user id: {userId}");
        }
        var (result, message) = _promotionService.PerformFootballerPromotion(HttpContext, _requestService.GetFootballerRequestById(userId));
        if (!result)
        {
            return BadRequest(message);
        }

        return Ok(message);
    }
    //TODO END FOOTBALLERS
    
    
    
    
    
    //TODO START COACHES
    [Authorize(Policy = "RequireAdminRole")]
    [HttpGet("coaches")]
    public IActionResult GetAllCoachRequests()
    {
        return Ok(_requestService.GetAllCoachRequests());
    }
    
    [Authorize(Policy = "RequireAdminRole")]
    [HttpGet("coaches/{userId:int}")]
    public IActionResult GetCoachRequestById(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest("Invalid ID");
        }
        
        return Ok(_requestService.GetCoachRequestById(userId));
    }
    
    [Authorize(Policy = "RequireAdminRole")]
    [HttpGet("coaches/{username}")]
    public IActionResult GetCoachRequestByUsername(string username)
    {
        return Ok(_requestService.GetCoachRequestByUsername(username));
    }
    
    [Authorize(Policy = "RequireAdminRole")]
    [HttpDelete("coaches/{id:int}")]
    public IActionResult DeleteCoachRequest(int id)
    {
        var result = _requestService.DeleteCoachRequestById(id);
        if (result)
        {
            return Ok($"Deleted coach with id {id}.");
        }

        return NotFound($"No coach with id {id} found.");
    }
    
    [Authorize(Policy = "RequireAdminRole")]
    [HttpDelete("coaches/{username}")]
    public IActionResult DeleteCoachRequest(string username)
    {
        var result = _requestService.DeleteCoachRequestByUsername(username);
        if (result)
        {
            return Ok($"Deleted coach with name {username}.");
        }

        return NotFound($"No coach with name {username} found.");
    }   
    
    [Authorize(Policy = "RequireAdminRole")]
    [HttpPut("coaches/{userId:int}")]
    public IActionResult PromoteUserToCoach(int userId)
    {
        var (result, message) = _promotionService.PerformCoachPromotion(_requestService.GetCoachRequestById(userId));
        if (!result)
        {
            return BadRequest(message);
        }

        return Ok(message);
    }   
    
}