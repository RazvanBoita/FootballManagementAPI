using FootballMgm.Api.Models;
using FootballMgm.Api.Repositories;

namespace FootballMgm.Api.Services;

public class RequestService : IRequestService
{
    private readonly IFootballerRequestsRepository _footballerRequestsRepository;
    private readonly ICoachRequestsRepository _coachRequestsRepository;

    public RequestService(IFootballerRequestsRepository footballerRequestsRepository, ICoachRequestsRepository coachRequestsRepository)
    {
        _footballerRequestsRepository = footballerRequestsRepository;
        _coachRequestsRepository = coachRequestsRepository;
    }
    
    
    //TODO START FOOTBALLERS
    public IEnumerable<FootballerRequest> GetAllFootballerRequests()
    {
        return _footballerRequestsRepository.GetAllFootballerRequests();
    }

    public FootballerRequest? GetFootballerRequestById(int userId)
    {
        return _footballerRequestsRepository.GetFootballerRequestByUserId(userId);
    }

    public FootballerRequest? GetFootballerRequestByUsername(string username)
    {
        return _footballerRequestsRepository.GetFootballerRequestByUsername(username);
    }

    public bool DeleteFootballerRequestById(int id)
    {
        var foundFootballerRequest = _footballerRequestsRepository.GetFootballerRequestById(id);
        if (foundFootballerRequest is null)
        {
            return false;
        }
        return _footballerRequestsRepository.DeleteFootballerRequestById(foundFootballerRequest);
    }

    public bool DeleteFootballerRequestByUsername(string username)
    {
        var foundFootballerRequest = _footballerRequestsRepository.GetFootballerRequestByUsername(username);
        if (foundFootballerRequest is null)
        {
            return false;
        }
        return _footballerRequestsRepository.DeleteFootballerRequestByUsername(foundFootballerRequest);
    }
    //TODO END FOOTBALLERS
    
    
    
    
    //TODO START COACHES

    public IEnumerable<CoachRequest> GetAllCoachRequests()
    {
        return _coachRequestsRepository.GetAllCoachRequests();
    }

    public CoachRequest GetCoachRequestById(int id)
    {
        return _coachRequestsRepository.GetCoachRequestByUserId(id);
    }

    public CoachRequest GetCoachRequestByUsername(string username)
    {
        return _coachRequestsRepository.GetCoachRequestByUsername(username);
    }

    public bool DeleteCoachRequestById(int id)
    {
        var foundCoachRequest = _coachRequestsRepository.GetCoachRequestByUserId(id);
        if (foundCoachRequest is null)
        {
            return false;
        }
        return _coachRequestsRepository.DeleteCoachRequestById(id).Item1;
    }

    public bool DeleteCoachRequestByUsername(string username)
    {
        var foundCoachRequest = _coachRequestsRepository.GetCoachRequestByUsername(username);
        if (foundCoachRequest is null)
        {
            return false;
        }
        
        return _coachRequestsRepository.DeleteCoachRequestByUsername(username).Item1;
    }

    //TODO END COACHES
}