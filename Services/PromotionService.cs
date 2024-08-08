using System.Security.Claims;
using FootballMgm.Api.Dtos;
using FootballMgm.Api.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FootballMgm.Api.Services;

public class PromotionService : IPromotionService
{
    private readonly ITeamRepository _teamRepository;
    private readonly IFootballerRepository _footballerRepository;
    private readonly IFootballerRequestsRepository _footballerRequestsRepository;
    public PromotionService(ITeamRepository teamRepository, IFootballerRepository footballerRepository, IFootballerRequestsRepository footballerRequestsRepository)
    {
        _teamRepository = teamRepository;
        _footballerRepository = footballerRepository;
        _footballerRequestsRepository = footballerRequestsRepository;
    }
    
    public (bool Success, string Message) FootballerRequestPromotion(HttpContext httpContext, FootballerDto footballerDto)
    {
        if (!_teamRepository.TeamExistsByName(footballerDto.TeamName))
        {
            return (false, $"Team: {footballerDto.TeamName} does not exist!");
        }
        
        var username = httpContext.User.FindFirst(ClaimTypes.Name)?.Value ?? "unknown";
        var uid = Convert.ToInt32(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        
        
        //TODO adaug in footballers table si fac conexiunea cu app users
        if (uid == 0)
        {
            return (false, "Current user can't be recognized");
        }

        if (_footballerRepository.FootballerExistsById(uid))
        {
            return (false, "User is already a footballer");
        }
        
        //TODO add footballer and details in the FootballerRequest
        
        //TODO reject if they already made a request(LATER CHECK FOR COACH REQUESTS AS WELL!)

        if (_footballerRequestsRepository.CheckFootballerRequestExistence(uid))
        {
            return (false, $"A request for username {username} has already been made!");
        }
        
        try
        {
            _footballerRequestsRepository.InsertFootballerRequest(uid, footballerDto);
        }
        catch(Exception e)
        {
            return (false, e.Message);
        }
        
        return (true, $"User {username} added as a footballer");
    }
}