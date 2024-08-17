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
            var footballerRequest = new FootballerRequest
            {
                UserId = uid,
                Position = footballerDto.Position,
                PrefferedFoot = footballerDto.PrefferedFoot,
                ShirtNumber = footballerDto.ShirtNumber,
                TeamName = footballerDto.TeamName
            };
            _footballerRequestsRepository.InsertFootballerRequest(footballerRequest);
        }
        catch(Exception e)
        {
            return (false, e.Message);
        }
        
        return (true, $"User with id {uid} completed a footballer request successfully!");
    }

    public (bool Success, string Message) PerformFootballerPromotion(HttpContext httpContext, FootballerRequest footballerRequest)
    {
        //TODO vezi daca e coach, poate sa faca treaba asta doar daca e din aceeasi echipa cu requestul
        var userDoingPromotionId = Convert.ToInt32(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        if (_coachRepository.CoachExistsById(userDoingPromotionId))
        {
            var coach = _coachRepository.GetCoachById(userDoingPromotionId);
            if (coach is null)
            {
                return (false, "No coach with given id");
            }
            
            var coachTeam = _teamRepository.GetTeamNameById(coach.TeamId ?? 0);
            if (coachTeam is null)
            {
                return (false, "No team found");
            }

            if (!coachTeam.Equals(footballerRequest.TeamName))
            {
                return (false, "Coach can only promote footballers belonging to their clubs");
            }
        }
        
        
        //TODO verifica fotbalistul daca are team id ok si daca exista

        if (_appUserRepository.GetUserById(footballerRequest.UserId) is null)
        {
            return (false, "No user found");
        }
        
        if (_footballerRepository.GetTeamIdForFootballer(footballerRequest.UserId) is null)
        {
            return (false, "User with given id doesn't have a team assigned.");
        }
        var footballerToAdd = new Footballer
        {
            UserId = footballerRequest.UserId,
            Position = footballerRequest.Position,
            PreferredFoot = footballerRequest.PrefferedFoot,
            ShirtNumber = footballerRequest.ShirtNumber,
            TeamId = _footballerRepository.GetTeamIdForFootballer(footballerRequest.UserId)
        };
        
        try
        {
            var resultToFootballerInsertion = _footballerRepository.InsertFootballer(footballerToAdd);
            
            var resultToRequestDeletion =
                _footballerRequestsRepository.DeleteFootballerRequestById(footballerRequest);
            
            var resultToRoleChange = _appUserRepository.ChangeUserRole(footballerRequest.UserId, "Footballer");
            
            if (resultToFootballerInsertion is false)
            {
                return (false, "Error: Inserting a footballer");
            }
            
            if (resultToRequestDeletion is false)
            {
                return (resultToRequestDeletion, "Error: Deleting a footballer request");
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
        
        var coachRequest = new CoachRequest()
        {
            UserId = uid,
            XpYears = coachDto.XpYears,
            TeamName = coachDto.TeamName
        };
        try
        {
            _coachRequestsRepository.InsertCoachRequest(coachRequest);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
        
        return (true, $"User with id {uid} completed a coach request successfully!");
    }

    public (bool Success, string Message) PerformCoachPromotion(CoachRequest coachRequest)
    {
        var foundCoachRequest = _coachRequestsRepository.GetCoachRequestByUserId(coachRequest.UserId);
        if (foundCoachRequest is null)
        {
            return (false, "Coach request not found");
        }

        if (_coachRepository.GetTeamIdByCoachId(coachRequest.UserId) is null)
        {
            return (false, "Coach doesn't have a team");
        }

        var coachToAdd = new Coach()
        {
            UserId = coachRequest.UserId,
            XpYears = coachRequest.XpYears,
            TeamId = _coachRepository.GetTeamIdByCoachId(coachRequest.UserId)
        };
        try
        {
            var resultToCoachInsertion = _coachRepository.InsertCoach(coachToAdd);
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