using FootballMgm.Api.Models;

namespace FootballMgm.Api.Services;

public interface IRequestService
{
    public IEnumerable<FootballerRequest> GetAllFootballerRequests();
    public FootballerRequest GetFootballerRequestById(int id);
    public FootballerRequest GetFootballerRequestByUsername(string username);
    public bool DeleteFootballerRequestById(int id);
    public bool DeleteFootballerRequestByUsername(string username);


    public IEnumerable<CoachRequest> GetAllCoachRequests();
    public CoachRequest GetCoachRequestById(int id);
    public CoachRequest GetCoachRequestByUsername(string username);
    public bool DeleteCoachRequestById(int id);
    public bool DeleteCoachRequestByUsername(string username);

}