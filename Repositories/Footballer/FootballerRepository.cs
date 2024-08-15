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

    public bool InsertFootballer(FootballerRequest footballerRequest)
    {
        //TODO get team id for the teamName
        var teamId = _dbContext.Teams.FirstOrDefault(t => t.Name == footballerRequest.TeamName)?.Id;
        if (teamId == 0 || teamId == null)
        {
            return false;
        }

        
        try
        {
            var footballer = new Footballer
            {
                UserId = footballerRequest.UserId,
                Position = footballerRequest.Position,
                PreferredFoot = footballerRequest.PrefferedFoot,
                ShirtNumber = footballerRequest.ShirtNumber,
                TeamId = teamId
            };
            
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
}