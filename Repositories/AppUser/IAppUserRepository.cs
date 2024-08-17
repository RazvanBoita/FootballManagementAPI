using FootballMgm.Api.Data;
using FootballMgm.Api.Models;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace FootballMgm.Api.Repositories;

public interface IAppUserRepository
{
    public AppUser? GetUserById(int id);
    public AppUser? GetUserByUsername(string username);
    public IEnumerable<AppUser> GetAllUsers();
    public void InsertUser(AppUser appUser);
    public Task InsertUserAsync(AppUser appUser);
    public void DeleteUser(AppUser appUser);
    public void DeleteUserById(int id);
    public void DeleteUserByUsername(string username);

    public bool ChangeUserRole(int userId, string newRole);

}