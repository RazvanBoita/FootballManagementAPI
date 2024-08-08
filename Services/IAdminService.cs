using FootballMgm.Api.Models;

namespace FootballMgm.Api.Services;

public interface IAdminService
{
    public IEnumerable<FootballerRequest> GetAllFootballerRequests();
    public FootballerRequest GetFootballerRequestById(int id);
    public FootballerRequest GetFootballerRequestByUsername(string username);
    public bool DeleteFootballerRequestById(int id);
    public bool DeleteFootballerRequestByUsername(string username);
    
}