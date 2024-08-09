using FootballMgm.Api.Dtos;
using FootballMgm.Api.Models;

namespace FootballMgm.Api.Services;

public interface IPromotionService
{
    public (bool Success, string Message) FootballerRequestPromotion(HttpContext httpContext,
        FootballerDto footballerDto);

    public (bool Success, string Message) PerformFootballerPromotion(HttpContext httpContext, FootballerRequest footballerRequest);
    
    public (bool Success, string Message) CoachRequestPromotion(HttpContext httpContext,
        CoachDto coachDto);

    public (bool Success, string Message) PerformCoachPromotion(CoachRequest coachRequest);
}