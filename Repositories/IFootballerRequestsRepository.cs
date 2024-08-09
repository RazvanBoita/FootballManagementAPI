using FootballMgm.Api.Dtos;
using FootballMgm.Api.Models;

namespace FootballMgm.Api.Repositories;

public interface IFootballerRequestsRepository
{
    public IEnumerable<FootballerRequest> GetAllFootballerRequests();
    public FootballerRequest GetFootballerRequestById(int id);
    public FootballerRequest GetFootballerRequestByUserId(int userId);
    public FootballerRequest GetFootballerRequestByUsername(string username);
    public void InsertFootballerRequest(FootballerRequest footballerRequest);
    public void InsertFootballerRequest(int userId, FootballerDto footballerDto);
    public bool CheckFootballerRequestExistence(int userId);
    public bool CheckFootballerRequestExistence(FootballerRequest footballerRequest);

    public (bool, string) DeleteFootballerRequestById(int userId);
    public (bool, string) DeleteFootballerRequestByUsername(string username);
}