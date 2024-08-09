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
    private readonly ICoachRequestsRepository _coachRequestsRepository;
    private readonly ICoachRepository _coachRepository;
    public PromotionService(ITeamRepository teamRepository, IFootballerRepository footballerRepository, IFootballerRequestsRepository footballerRequestsRepository, IAppUserRepository appUserRepository, ICoachRequestsRepository coachRequestsRepository, ICoachRepository coachRepository)
    {
        _teamRepository = teamRepository;
        _footballerRepository = footballerRepository;
        _footballerRequestsRepository = footballerRequestsRepository;
        _appUserRepository = appUserRepository;
        _coachRequestsRepository = coachRequestsRepository;
        _coachRepository = coachRepository;
    }
    
    public (bool Success, string Message) FootballerRequestPromotion(HttpContext httpContext, FootballerDto footballerDto)
    {
        
        var uid = Convert.ToInt32(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var (result, message) = TreatEdgeCases(uid, footballerDto.TeamName);

        if (result is false)
        {
            return (result, message);
        }
        
        try
        {
            _footballerRequestsRepository.InsertFootballerRequest(uid, footballerDto);
        }
        catch(Exception e)
        {
            return (false, e.Message);
        }
        
        return (true, $"User with id {uid} completed a footballer request successfully!");
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
            
            if (resultToRequestDeletion.Item1 is false)
            {
                return resultToRequestDeletion;
            }
            
            if (resultToRoleChange is false)
            {
                return (false, "Error: Changing user role");
            }
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }

        return (true, $"User with id {footballerRequest.UserId} promoted to a footballer");
    }


    public (bool Success, string Message) CoachRequestPromotion(HttpContext httpContext, CoachDto coachDto)
    {
        var uid = Convert.ToInt32(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var (result, message) = TreatEdgeCases(uid, coachDto.TeamName);

        if (result is false)
        {
            return (result, message);
        }
        
        try
        {
            _coachRequestsRepository.InsertCoachRequest(uid, coachDto);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
        
        return (true, $"User with id {uid} completed a coach request successfully!");
    }

    public (bool Success, string Message) PerformCoachPromotion(CoachRequest coachRequest)
    {
        try
        {
            var resultToCoachInsertion = _coachRepository.InsertCoach(coachRequest);
            var resultToRoleChange = _appUserRepository.ChangeUserRole(coachRequest.UserId, "Coach");
            var resultToRequestDeletion = _coachRequestsRepository.DeleteCoachRequestById(coachRequest.UserId);
            if (resultToCoachInsertion is false)
            {
                return (false, "Error: Inserting a coach");
            }
            
            if (resultToRequestDeletion.Item1 is false)
            {
                return resultToRequestDeletion;
            }
            
            if (resultToRoleChange is false)
            {
                return (false, "Error: Changing user role");
            }
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
        return (true, $"User with id {coachRequest.UserId} promoted to a coach");
    }


    private (bool Success, string Message) TreatEdgeCases(int uid, string teamName)
    {
        if (!_teamRepository.TeamExistsByName(teamName))
        {
            return (false, $"Team: {teamName} does not exist!");
        }
        
        if (uid == 0)
        {
            return (false, "Current user can't be recognized");
        }
        
        if (_footballerRequestsRepository.CheckFootballerRequestExistence(uid) || _coachRequestsRepository.CheckCoachRequestExistence(uid))
        {
            return (false, $"A request for user id: {uid} has already been made!");
        }
        
        if (_footballerRepository.FootballerExistsById(uid))
        {
            return (false, "User is already a footballer");
        }

        if (_coachRepository.CoachExistsById(uid))
        {
            return (false, "User is already a coach");
        }

        return (true, "Ok");
    }
}