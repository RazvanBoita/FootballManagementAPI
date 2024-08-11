using System.ComponentModel.DataAnnotations;

namespace FootballMgm.Api.Models;

public class Announcement
{
    public int Id { get; set; }
    
    [StringLength(250)]
    public string Content { get; set; }
    
    //nu mi trebuie Coach(author) pt ca oricum doar el poate sa faca un anunt pt echipa respectiva
    
    public DateTime Date { get; set; }
    
    public int TeamId { get; set; }
    public Team Team { get; set; }
}