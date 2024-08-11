

namespace FootballMgm.Api.Repositories.Announcement;

public interface IAnnouncementRepository
{
    public (bool, string) InsertAnnouncement(Models.Announcement announcement);
    public bool DeleteAnnouncement(int id);
    public IEnumerable<Models.Announcement> GetAllAnnouncements(int teamId);
}