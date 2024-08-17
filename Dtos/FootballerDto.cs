using System.ComponentModel.DataAnnotations;
using FootballMgm.Api.Models;
using FootballMgm.Api.Validators.FootballValidators;

namespace FootballMgm.Api.Dtos;

public class FootballerDto
{
    public char PreferredFoot { get; set; }
    
    public FootballPosition Position { get; set; }
    
    public int ShirtNumber { get; set; }
    
    public string TeamName { get; set; }
}