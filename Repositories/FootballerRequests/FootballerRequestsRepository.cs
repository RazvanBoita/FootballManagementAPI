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

    public IEnumerable<FootballerRequest> GetAllFootballerRequests()
    {
        return _dbContext.FootballerRequests.ToList();
    }

    public FootballerRequest? GetFootballerRequestById(int id)
    {
        return _dbContext.FootballerRequests.FirstOrDefault(fr => fr.Id == id);
    }
    public FootballerRequest? GetFootballerRequestByUsername(string username)
    {
        return _dbContext.FootballerRequests.FirstOrDefault(fr => fr.User.Username == username);
    }

    public FootballerRequest? GetFootballerRequestByUserId(int userId)
    {
        return _dbContext.FootballerRequests.FirstOrDefault(fr => fr.UserId == userId);
    }

    public void InsertFootballerRequest(FootballerRequest footballerRequest)
    {
        _dbContext.FootballerRequests.Add(footballerRequest);
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

    public bool DeleteFootballerRequestById(FootballerRequest foundFootballerRequest)
    {
        try
        {
            _dbContext.FootballerRequests.Remove(foundFootballerRequest);
            _dbContext.SaveChanges();
        }
        catch (Exception e)
        {
            return false;
        }
        
        return true;
    }

    public bool DeleteFootballerRequestByUsername(FootballerRequest foundFootballerRequest)
    {
        try
        {
            _dbContext.FootballerRequests.Remove(foundFootballerRequest);
            _dbContext.SaveChanges();
        }
        catch (Exception e)
        {
            return false;
        }
        
        return true;
    }
}