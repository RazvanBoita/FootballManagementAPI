namespace FootballMgm.Api.Services.Announcement;

public interface IAnnouncementService
{
    public (int, string) ProcessAnnouncement(HttpContext httpContext, string Content);
    public (int, string, IEnumerable<Models.Announcement>) GetAnnouncements(HttpContext httpContext);
}