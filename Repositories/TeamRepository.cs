using FootballMgm.Api.Data;

namespace FootballMgm.Api.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly FootballDbContext _dbContext;
    public TeamRepository(FootballDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool TeamExistsByName(string teamName)
    {
        return _dbContext.Teams.Any(t => string.Equals(t.Name.ToLower(),teamName.ToLower()));
    }
}