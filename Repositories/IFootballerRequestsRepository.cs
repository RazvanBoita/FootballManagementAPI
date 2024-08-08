using FootballMgm.Api.Dtos;
using FootballMgm.Api.Models;

namespace FootballMgm.Api.Repositories;

public interface IFootballerRequestsRepository
{
    public void InsertFootballerRequest(FootballerRequest footballerRequest);
    public void InsertFootballerRequest(int userId, FootballerDto footballerDto);
    public bool CheckFootballerRequestExistence(int userId);
    public bool CheckFootballerRequestExistence(FootballerRequest footballerRequest);
}