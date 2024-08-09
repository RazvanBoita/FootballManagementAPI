using FootballMgm.Api.Data;
using FootballMgm.Api.Models;

namespace FootballMgm.Api.Repositories;

public class CoachRepository : ICoachRepository
{
    private readonly FootballDbContext _dbContext;

    public CoachRepository(FootballDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    public bool CoachExistsById(int id)
    {
        return _dbContext.Coaches.Any(c => c.UserId == id);
    }

    public bool CoachExistsByUsername(string username)
    {
        return _dbContext.Coaches.Any(c => c.User.Username == username);
    }

    public bool InsertCoach(CoachRequest coachRequest)
    {
        var teamId = _dbContext.Teams.FirstOrDefault(t => t.Name == coachRequest.TeamName)?.Id;
        if (teamId == 0 || teamId == null)
        {
            return false;
        }

        try
        {
            var coach = new Coach
            {
                UserId = coachRequest.UserId,
                XpYears = coachRequest.XpYears,
                TeamId = teamId
            };
            _dbContext.Coaches.Add(coach);
            _dbContext.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }

        return true;
    }
}