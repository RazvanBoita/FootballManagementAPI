namespace FootballMgm.Api.Models;

public class AppUser
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; }
    public ICollection<FootballerRequest> FootballerRequests { get; set; }
}