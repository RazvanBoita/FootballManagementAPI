using System.Security.Claims;
using FootballMgm.Api.Repositories;
using FootballMgm.Api.Repositories.Announcement;

namespace FootballMgm.Api.Services.Announcement;

public class AnnouncementService : IAnnouncementService
{
    private readonly ICoachRepository _coachRepository;
    private readonly IFootballerRepository _footballerRepository;
    private readonly IAnnouncementRepository _announcementRepository;

    public AnnouncementService(ICoachRepository coachRepository, IAnnouncementRepository announcementRepository, IFootballerRepository footballerRepository)
    {
        _coachRepository = coachRepository;
        _announcementRepository = announcementRepository;
        _footballerRepository = footballerRepository;
    }
    
    
    public (int, string) ProcessAnnouncement(HttpContext httpContext, string Content)
    {
        //iau id-ul din request al coach-ului.
        var userId = Convert.ToInt32(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        if (userId == 0)
        {
            return (400, "User making the request couldn't be found");
        }
        //iau teamname ul pt id respectiv
        var coach = _coachRepository.GetCoachById(userId);
        if (coach is null)
        {
            return (400, "User making the request must be a coach");
        }

        if (coach.TeamId is null)
        {
            return (400, "User making the request must be a coach signed with an existing team");
        }

        if (Content is "" or null)
        {
            return (400, "Content must not be empty");
        }

        var status = _announcementRepository.InsertAnnouncement(new Models.Announcement
        {
            Content = Content,
            Date = DateTime.Now,
            TeamId = coach.TeamId!.Value
        });
        return status.Item1 ? (200, status.Item2) : (500, status.Item2); 
    }

    public (int, string, IEnumerable<Models.Announcement>?) GetAnnouncements(HttpContext httpContext)
    {
        var userId = Convert.ToInt32(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var maybeCoach = _coachRepository.GetCoachById(userId);
        var maybeFootballer = _footballerRepository.GetFootballerById(userId);
        var finalTeamId = 0;
        if (maybeCoach is not null)
        {
            finalTeamId = maybeCoach.TeamId.Value;
        }
        else if (maybeFootballer is not null)
        {
            finalTeamId = maybeFootballer.TeamId.Value;
        }
        else
        {
            return (400, "No user found", null);
        }

        return (200, "Teams found", _announcementRepository.GetAllAnnouncements(finalTeamId));
    }
}