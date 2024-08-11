using System.ComponentModel;
using System.Security.Claims;
using FootballMgm.Api.Dtos;
using FootballMgm.Api.Services.Announcement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FootballMgm.Api.Controllers;
    
    
[Route("api/[controller]")]
[ApiController]
public class AnnouncementController : ControllerBase
{
    private readonly IAnnouncementService _announcementService;

    public AnnouncementController(IAnnouncementService announcementService)
    {
        _announcementService = announcementService;
    }
    
    
    //TODO Post din partea unui coach. Announcementul va fi mapat echipei pe care o detine coach ul automat
    [Authorize(Policy = "RequireCoachRole")]
    [HttpPost]
    [SwaggerOperation(
        Summary = "Creation of an announcement, only done by a coach",
        Description = "Only a coach can make an announcement, and that gets automatically mapped to the coach's team channel. Footballers belonging to that team can then see those announcements." 
    )]
    public IActionResult MakeAnnouncement([FromBody] AnnouncementDto announcementDto)
    {
        var result = _announcementService.ProcessAnnouncement(HttpContext, announcementDto.Content);
        return StatusCode(result.Item1, new {message = result.Item2});
    }
    
    //TODO Delete din partea unui coach pt un anumit announcement
    [Authorize(Policy = "RequireCoachRole")]
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Removes an announcement made by a coach.",
        Description = "A coach or an admin can delete an announcement. It will automatically delete itself after 14 days since posting."
    )]
    public IActionResult RemoveAnnouncement(string id)
    {
        return Ok(id);
    }
    
    
    
    //TODO Get din partea unui fotbalist, care poate sa vada doar anunturile de la echipa sa
    [Authorize(Policy = "RequireFootballerOrCoachRole")]
    [HttpGet]
    [SwaggerOperation(
        Summary = "Players can see announcements made by their coach",
        Description = "They can only see the announcements within their team."
    )]
    public IActionResult ReadAnnouncements()
    {
        var result = _announcementService.GetAnnouncements(HttpContext);
        return StatusCode(result.Item1, new
        {
            announcements = result.Item3
        });
    }
    
}

