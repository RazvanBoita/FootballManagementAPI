

namespace FootballMgm.Api.Repositories.Announcement;

public interface IAnnouncementRepository
{
    public (bool, string) InsertAnnouncement(Models.Announcement announcement);
    public Models.Announcement? GetAnnouncementById(int announcementId);
    public bool DeleteAnnouncement(Models.Announcement announcement);
    public IEnumerable<Models.Announcement> GetAllAnnouncements(int teamId);
}