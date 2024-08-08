namespace FootballMgm.Api.Dtos;

public class AppUserDto
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; }
}