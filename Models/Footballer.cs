using System.ComponentModel.DataAnnotations;

namespace FootballMgm.Api.Models;

public class Footballer
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public AppUser User { get; set; }
    public char PreferredFoot { get; set; }
    public FootballPosition Position { get; set; }
    [Range(1, 99)]
    public int ShirtNumber { get; set; }
    public int? TeamId { get; set; }
    public Team Team { get; set; }
}