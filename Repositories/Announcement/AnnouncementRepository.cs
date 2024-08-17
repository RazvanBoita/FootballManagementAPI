using System.Runtime.InteropServices.JavaScript;
using FootballMgm.Api.Data;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.OpenApi.Any;

namespace FootballMgm.Api.Repositories.Announcement;

public class AnnouncementRepository : IAnnouncementRepository
{
    private readonly FootballDbContext _dbContext;

    public AnnouncementRepository(FootballDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    public (bool, string) InsertAnnouncement(Models.Announcement announcement)
    {
        try
        {
            _dbContext.Announcements.Add(announcement);
            _dbContext.SaveChanges();
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
        return (true, "Announcement added");
    }
    

    public IEnumerable<Models.Announcement> GetAllAnnouncements(int teamId)
    {
        return _dbContext.Announcements.Where(a => a.TeamId == teamId).ToList();
    }

    public Models.Announcement? GetAnnouncementById(int announcementId)
    {
        throw new NotImplementedException();
    }

    public bool DeleteAnnouncement(Models.Announcement announcement)
    {
        throw new NotImplementedException();
    }
}