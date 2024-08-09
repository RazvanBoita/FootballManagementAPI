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
    private readonly IRequestService _requestService;
    private readonly IPromotionService _promotionService;
    public RequestController(IRequestService requestService, IPromotionService promotionService)
    {
        _requestService = requestService;
        _promotionService = promotionService;
    }
    
    
    //TODO START FOOTBALLERS
    [HttpGet]
    [Route("footballers")]
    public IActionResult GetAllFootballerRequests()
    {
        return Ok(_requestService.GetAllFootballerRequests());
    }

    
    
    [HttpGet("footballers/{userId:int}")]
    public IActionResult GetFootballerRequestById(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest("Invalid ID");
        }
        
        return Ok(_requestService.GetFootballerRequestById(userId));
    }

    
    
    [HttpGet("footballers/{username}")]
    public IActionResult GetFootballerRequestByUsername(string username)
    {
        return Ok(_requestService.GetFootballerRequestByUsername(username));
    }
    
    

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

    [HttpPut("footballers/{userId:int}")]
    public IActionResult PromoteUserToFootballer(int userId)
    {
        var (result, message) = _promotionService.PerformFootballerPromotion(_requestService.GetFootballerRequestById(userId));
        if (!result)
        {
            return BadRequest(message);
        }

        return Ok(message);
    }
    //TODO END FOOTBALLERS
    
    
    
    
    
    //TODO START COACHES
    [HttpGet("coaches")]
    public IActionResult GetAllCoachRequests()
    {
        return Ok(_requestService.GetAllCoachRequests());
    }
    
    [HttpGet("coaches/{userId:int}")]
    public IActionResult GetCoachRequestById(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest("Invalid ID");
        }
        
        return Ok(_requestService.GetCoachRequestById(userId));
    }
    
    [HttpGet("coaches/{username}")]
    public IActionResult GetCoachRequestByUsername(string username)
    {
        return Ok(_requestService.GetCoachRequestByUsername(username));
    }
    
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