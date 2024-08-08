using FootballMgm.Api.Data;
using FootballMgm.Api.Dtos;
using FootballMgm.Api.Models;

namespace FootballMgm.Api.Repositories;

public class FootballerRequestsRepository : IFootballerRequestsRepository
{
    private readonly FootballDbContext _dbContext;

    public FootballerRequestsRepository(FootballDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void InsertFootballerRequest(FootballerRequest footballerRequest)
    {
        _dbContext.FootballerRequests.Add(footballerRequest);
        _dbContext.SaveChanges();
    }

    public void InsertFootballerRequest(int userId, FootballerDto footballerDto)
    {
        _dbContext.FootballerRequests.Add(new FootballerRequest
        {
            UserId = userId,
            Position = footballerDto.Position,
            PrefferedFoot = footballerDto.PrefferedFoot,
            ShirtNumber = footballerDto.ShirtNumber,
            TeamName = footballerDto.TeamName
        });
        _dbContext.SaveChanges();
    }

    public bool CheckFootballerRequestExistence(int userId)
    {
        return _dbContext.FootballerRequests.Any(fr => fr.UserId == userId);
    }

    public bool CheckFootballerRequestExistence(FootballerRequest footballerRequest)
    {
        return _dbContext.FootballerRequests.Any(fr => fr.UserId == footballerRequest.UserId);
    }
}