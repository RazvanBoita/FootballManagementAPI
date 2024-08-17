using FluentValidation;
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
    private readonly IValidator<CoachDto> _coachValidator;
    public CoachController(IPromotionService promotionService, IValidator<CoachDto> coachValidator)
    {
        _promotionService = promotionService;
        _coachValidator = coachValidator;
    }
    
    [Authorize(Policy = "RequireBaseRole")]
    [HttpPost]
    public IActionResult RequestPromotion([FromBody] CoachDto coachDto)
    {
        var validationResult = _coachValidator.Validate(coachDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var (result, msg) = _promotionService.CoachRequestPromotion(HttpContext, coachDto);
        if (result)
        {
            return Ok(msg);
        }

        return BadRequest(msg);
    }
}