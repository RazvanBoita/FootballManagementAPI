using FootballMgm.Api.Dtos;

namespace FootballMgm.Api.Services;

public interface IPromotionService
{
    public (bool Success, string Message) FootballerRequestPromotion(HttpContext httpContext,
        FootballerDto footballerDto);
}