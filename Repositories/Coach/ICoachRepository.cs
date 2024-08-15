using FootballMgm.Api.Models;

namespace FootballMgm.Api.Repositories;

public interface ICoachRepository
{
    public Coach? GetCoachById(int id);
    public bool CoachExistsById(int id);
    public bool CoachExistsByUsername(string username);
    public bool InsertCoach(CoachRequest coachRequest);
}