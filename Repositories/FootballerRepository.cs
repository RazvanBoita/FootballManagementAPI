using FootballMgm.Api.Data;

namespace FootballMgm.Api.Repositories;

public class FootballerRepository : IFootballerRepository
{
    private readonly FootballDbContext _dbContext;
    public FootballerRepository(FootballDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    public bool FootballerExistsByName(string username)
    {
        return _dbContext.Footballers.Any(f => f.User.Username == username);
    }

    public bool FootballerExistsById(int id)
    {
        return _dbContext.Footballers.Any(f => f.Id == id);
    }
}