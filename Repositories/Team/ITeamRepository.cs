namespace FootballMgm.Api.Repositories;

public interface ITeamRepository
{
    public bool TeamExistsByName(string teamName);
    public string? GetTeamNameById(int id);
}