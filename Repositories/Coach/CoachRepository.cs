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

    public Coach? GetCoachById(int id)
    {
        return _dbContext.Coaches.FirstOrDefault(c => c.UserId == id);
    }

    public bool CoachExistsById(int id)
    {
        return _dbContext.Coaches.Any(c => c.UserId == id);
    }

    public bool CoachExistsByUsername(string username)
    {
        return _dbContext.Coaches.Any(c => c.User.Username == username);
    }

    public bool InsertCoach(Coach coach)
    {
        try
        {
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

    public int? GetTeamIdByCoachId(int coachId)
    {
        return _dbContext.Coaches.FirstOrDefault(c => c.UserId == coachId)?.TeamId;
    }
}