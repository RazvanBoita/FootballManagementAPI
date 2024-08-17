using FootballMgm.Api.Data;
using FootballMgm.Api.Models;

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
        return _dbContext.Footballers.Any(f => f.UserId == id);
    }

    public bool InsertFootballer(Footballer footballer)
    {
        try
        {
            _dbContext.Footballers.Add(footballer);
            _dbContext.SaveChanges();
        }
        catch (Exception e)
        {
            return false;
        }
        
        return true;
    }

    public Footballer? GetFootballerById(int id)
    {
        return _dbContext.Footballers.FirstOrDefault(f => f.UserId == id);
    }

    public int? GetTeamIdForFootballer(int footballerId)
    {
        return _dbContext.Footballers.FirstOrDefault(f => f.UserId == footballerId)?.TeamId;
    }
}