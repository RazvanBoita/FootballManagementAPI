using FootballMgm.Api.Models;

namespace FootballMgm.Api.Repositories;

public interface ICoachRepository
{
    public bool CoachExistsById(int id);
    public bool CoachExistsByUsername(string username);
    public bool InsertCoach(CoachRequest coachRequest);
}