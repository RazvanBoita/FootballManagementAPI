using FootballMgm.Api.Models;
using FootballMgm.Api.Validators.FootballValidators;

namespace FootballMgm.Api.Dtos;

public class FootballerDto
{
    [ValidPreferredFoot]
    public char PrefferedFoot { get; set; }
    
    public FootballPosition Position { get; set; }
    
    [ValidShirtNo]
    public int ShirtNumber { get; set; }
    
    public string TeamName { get; set; }
}