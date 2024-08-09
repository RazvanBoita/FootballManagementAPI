using System.ComponentModel.DataAnnotations;

namespace FootballMgm.Api.Dtos;

public class CoachDto
{
    [Range(0, 51)]
    public int XpYears { get; set; }
    public string TeamName { get; set; }
}