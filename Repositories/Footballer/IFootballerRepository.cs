using FootballMgm.Api.Models;

namespace FootballMgm.Api.Repositories;

public interface IFootballerRepository
{
    public bool FootballerExistsByName(string username);
    public bool FootballerExistsById(int id);

    public bool InsertFootballer(FootballerRequest footballer);
    public Footballer? GetFootballerById(int id);
}