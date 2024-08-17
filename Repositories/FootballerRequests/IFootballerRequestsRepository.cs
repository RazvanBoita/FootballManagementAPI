using FootballMgm.Api.Dtos;
using FootballMgm.Api.Models;

namespace FootballMgm.Api.Repositories;

public interface IFootballerRequestsRepository
{
    public IEnumerable<FootballerRequest> GetAllFootballerRequests();
    public FootballerRequest? GetFootballerRequestById(int id);
    public FootballerRequest? GetFootballerRequestByUserId(int userId);
    public FootballerRequest? GetFootballerRequestByUsername(string username);
    public void InsertFootballerRequest(FootballerRequest footballerRequest);
    public bool CheckFootballerRequestExistence(int userId);
    public bool CheckFootballerRequestExistence(FootballerRequest footballerRequest);

    public bool DeleteFootballerRequestById(FootballerRequest footballerRequest);
    public bool DeleteFootballerRequestByUsername(FootballerRequest footballerRequest);
}