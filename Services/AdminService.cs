using FootballMgm.Api.Models;
using FootballMgm.Api.Repositories;

namespace FootballMgm.Api.Services;

public class AdminService : IAdminService
{
    private readonly IFootballerRequestsRepository _footballerRequestsRepository;

    public AdminService(IFootballerRequestsRepository footballerRequestsRepository)
    {
        _footballerRequestsRepository = footballerRequestsRepository;
    }
    
    public IEnumerable<FootballerRequest> GetAllFootballerRequests()
    {
        return _footballerRequestsRepository.GetAllFootballerRequests();
    }

    public FootballerRequest GetFootballerRequestById(int userId)
    {
        return _footballerRequestsRepository.GetFootballerRequestByUserId(userId);
    }

    public FootballerRequest GetFootballerRequestByUsername(string username)
    {
        return _footballerRequestsRepository.GetFootballerRequestByUsername(username);
    }

    public bool DeleteFootballerRequestById(int id)
    {
        return _footballerRequestsRepository.DeleteFootballerRequestById(id);
    }

    public bool DeleteFootballerRequestByUsername(string username)
    {
        return _footballerRequestsRepository.DeleteFootballerRequestByUsername(username);
    }
}