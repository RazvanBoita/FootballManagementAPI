using System.Drawing.Drawing2D;
using System.Security.Claims;
using FootballMgm.Api.Dtos;
using FootballMgm.Api.Models;
using FootballMgm.Api.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace FootballMgm.Api.Services;

public class PromotionService : IPromotionService
{
    private readonly ITeamRepository _teamRepository;
    private readonly IFootballerRepository _footballerRepository;
    private readonly IFootballerRequestsRepository _footballerRequestsRepository;
    private readonly IAppUserRepository _appUserRepository;
    public PromotionService(ITeamRepository teamRepository, IFootballerRepository footballerRepository, IFootballerRequestsRepository footballerRequestsRepository, IAppUserRepository appUserRepository)
    {
        _teamRepository = teamRepository;
        _footballerRepository = footballerRepository;
        _footballerRequestsRepository = footballerRequestsRepository;
        _appUserRepository = appUserRepository;
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
        
        return (true, $"User {username} completed a footballer request successfully!");
    }

    public (bool Success, string Message) PerformFootballerPromotion(FootballerRequest footballerRequest)
    {
        try
        {
            var resultToFootballerInsertion = _footballerRepository.InsertFootballer(footballerRequest);
            var resultToRequestDeletion =
                _footballerRequestsRepository.DeleteFootballerRequestById(footballerRequest.UserId);
            var resultToRoleChange = _appUserRepository.ChangeUserRole(footballerRequest.UserId, "Footballer");
            if (resultToFootballerInsertion is false)
            {
                return (false, "Error: Inserting a footballer");
            }
            
            if (resultToRequestDeletion is false)
            {
                return (false, "Error: Deleting a request");
            }
            
            if (resultToRoleChange is false)
            {
                return (false, "Error:Changing user role");
            }
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }

        return (true, $"User with id {footballerRequest.UserId} promoted to a footballer");
    }
    
}