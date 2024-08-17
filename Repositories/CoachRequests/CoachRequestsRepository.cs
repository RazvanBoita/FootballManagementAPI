using FootballMgm.Api.Data;
using FootballMgm.Api.Dtos;
using FootballMgm.Api.Models;

namespace FootballMgm.Api.Repositories;

public class CoachRequestsRepository : ICoachRequestsRepository
{
    private readonly FootballDbContext _dbContext;

    public CoachRequestsRepository(FootballDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IEnumerable<CoachRequest> GetAllCoachRequests()
    {
        return _dbContext.CoachRequests.ToList();
    }

    public CoachRequest? GetCoachRequestByUserId(int userId)
    {
        return _dbContext.CoachRequests.FirstOrDefault(cr => cr.UserId == userId);
    }

    public CoachRequest? GetCoachRequestByUsername(string username)
    {
        return _dbContext.CoachRequests.FirstOrDefault(cr => cr.User.Username == username);
    }

    public bool InsertCoachRequest(CoachRequest coachRequest)
    {
        try
        {
            _dbContext.CoachRequests.Add(coachRequest);
            _dbContext.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error adding coach request: " + e.Message);
            return false;
        }
        return true;
    }

    public bool CheckCoachRequestExistence(int userId)
    {
        return _dbContext.CoachRequests.Any(cr => cr.UserId == userId);
    }

    public (bool, string) DeleteCoachRequestById(int userId)
    {
        var foundCoachRequest = _dbContext.CoachRequests.FirstOrDefault(cr => cr.UserId == userId);

        try
        {
            _dbContext.CoachRequests.Remove(foundCoachRequest!);
            _dbContext.SaveChanges();
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
        
        return (true, "Ok");
    }

    public (bool, string) DeleteCoachRequestByUsername(string username)
    {
        var foundCoachRequest = _dbContext.CoachRequests.FirstOrDefault(cr => cr.User.Username == username);
        try
        {
            _dbContext.CoachRequests.Remove(foundCoachRequest!);
            _dbContext.SaveChanges();
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
        
        return (true, "Ok");
    }
}