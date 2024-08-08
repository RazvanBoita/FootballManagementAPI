using System.Security.Claims;
using FootballMgm.Api.Dtos;
using FootballMgm.Api.Repositories;
using FootballMgm.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballMgm.Api.Controllers;

[ApiController]
[Route("api/footballer")]

public class FootballerController : ControllerBase
{

    private readonly IPromotionService _promotionService; 
    public FootballerController(IPromotionService promotionService)
    {
        _promotionService = promotionService;
    }
    
    [Authorize(Policy = "RequireBaseRole")]
    [HttpPost]
    public IActionResult RequestPromotion([FromBody] FootballerDto footballerDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Model state invalid");
        }

        var (result, msg) = _promotionService.FootballerRequestPromotion(HttpContext, footballerDto);
        if (result)
        {
            return Ok(msg);
        }

        return BadRequest(msg);
    }
}