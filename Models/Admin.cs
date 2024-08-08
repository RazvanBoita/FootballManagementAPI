namespace FootballMgm.Api.Models;

public class Admin
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public AppUser User { get; set; }
}