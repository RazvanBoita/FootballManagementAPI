using FootballMgm.Api.Dtos;
using FootballMgm.Api.Models;

namespace FootballMgm.Api.Repositories;

public interface ICoachRequestsRepository
{
    public IEnumerable<CoachRequest> GetAllCoachRequests();
    public CoachRequest GetCoachRequestByUserId(int userId);
    public CoachRequest GetCoachRequestByUsername(string username);
    public bool InsertCoachRequest(int userId, CoachDto coachDto);
    public bool CheckCoachRequestExistence(int userId);
    public (bool, string) DeleteCoachRequestById(int userId);
    public (bool, string) DeleteCoachRequestByUsername(string username);
}