using System.ComponentModel.DataAnnotations;

namespace FootballMgm.Api.Models;

public class Coach
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public AppUser User { get; set; }
    [Range(0, 51)]
    public int XpYears { get; set; }
    public int? TeamId { get; set; }
    public Team Team { get; set; }
}