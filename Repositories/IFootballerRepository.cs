namespace FootballMgm.Api.Repositories;

public interface IFootballerRepository
{
    public bool FootballerExistsByName(string username);
    public bool FootballerExistsById(int id);
}