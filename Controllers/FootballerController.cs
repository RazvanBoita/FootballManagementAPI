using System.Security.Claims;
using FluentValidation;
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
    private readonly IValidator<FootballerDto> _footballerValidator;
    public FootballerController(IPromotionService promotionService, IValidator<FootballerDto> footballerValidator)
    {
        _promotionService = promotionService;
        _footballerValidator = footballerValidator;
    }
    
    [Authorize(Policy = "RequireBaseRole")]
    [HttpPost]
    public IActionResult RequestPromotion([FromBody] FootballerDto footballerDto)
    {
        var validationResult = _footballerValidator.Validate(footballerDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var (result, msg) = _promotionService.FootballerRequestPromotion(HttpContext, footballerDto);
        if (result)
        {
            return Ok(msg);
        }

        return BadRequest(msg);
    }
}