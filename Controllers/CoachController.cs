using FootballMgm.Api.Dtos;
using FootballMgm.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballMgm.Api.Controllers;

[ApiController]
[Route("api/coach")]
public class CoachController : ControllerBase
{
    private readonly IPromotionService _promotionService; 
    public CoachController(IPromotionService promotionService)
    {
        _promotionService = promotionService;
    }
    
    [Authorize(Policy = "RequireBaseRole")]
    [HttpPost]
    public IActionResult RequestPromotion([FromBody] CoachDto coachDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Model state invalid");
        }

        var (result, msg) = _promotionService.CoachRequestPromotion(HttpContext, coachDto);
        if (result)
        {
            return Ok(msg);
        }

        return BadRequest(msg);
    }
}