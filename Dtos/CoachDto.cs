using System.ComponentModel.DataAnnotations;

namespace FootballMgm.Api.Dtos;

public class CoachDto
{
    public int XpYears { get; set; }
    public string TeamName { get; set; }
}