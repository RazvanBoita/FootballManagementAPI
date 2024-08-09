using FootballMgm.Api.Dtos;

namespace FootballMgm.Api.Models;

public class CoachRequest : CoachDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public AppUser User { get; set; }
}