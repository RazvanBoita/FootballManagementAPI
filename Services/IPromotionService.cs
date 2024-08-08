using FootballMgm.Api.Dtos;
using FootballMgm.Api.Models;

namespace FootballMgm.Api.Services;

public interface IPromotionService
{
    public (bool Success, string Message) FootballerRequestPromotion(HttpContext httpContext,
        FootballerDto footballerDto);

    public (bool Success, string Message) PerformFootballerPromotion(FootballerRequest footballerRequest);
}