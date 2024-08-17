using FootballMgm.Api.Data;
using FootballMgm.Api.Models;

namespace FootballMgm.Api.Repositories;

public class AppUserRepository : IAppUserRepository
{
    private readonly FootballDbContext _dbContext;
    public AppUserRepository(FootballDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public AppUser? GetUserById(int id)
    {
        return _dbContext.Users.Find(id);
    }

    public AppUser? GetUserByUsername(string username)
    {
        return _dbContext.Users.FirstOrDefault(u => u.Username == username);
    }

    public IEnumerable<AppUser> GetAllUsers()
    {
        return _dbContext.Users.ToList();
    }

    public void DeleteUser(AppUser appUser)
    {
        throw new NotImplementedException();
    }

    public void DeleteUserById(int id)
    {
        throw new NotImplementedException();
    }

    public void DeleteUserByUsername(string username)
    {
        throw new NotImplementedException();
    }

    public void InsertUser(AppUser appUser)
    {
        _dbContext.Users.Add(appUser);
        _dbContext.SaveChanges();
    }

    public async Task InsertUserAsync(AppUser appUser)
    {
        await _dbContext.Users.AddAsync(appUser);
        await _dbContext.SaveChangesAsync();
    }

    public bool ChangeUserRole(int userId, string newRole)
    {
        var foundUser = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
        string[] roles = { "Footballer", "Coach", "Admin" };
        
        if (!roles.Contains(newRole))
        {
            return false;
        }
        try
        {
            foundUser!.Role = newRole;
            _dbContext.SaveChanges();
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }
}