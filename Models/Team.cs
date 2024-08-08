using FootballMgm.Api.Data;

namespace FootballMgm.Api.Models;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Coach Coach { get; set; }
    public ICollection<Footballer> Footballers { get; set; }
}