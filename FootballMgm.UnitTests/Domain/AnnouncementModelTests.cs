using System.ComponentModel.DataAnnotations;
using FootballMgm.Api.Models;

namespace FootballMgm.UnitTests.Domain;

public class AnnouncementModelTests
{
    [Fact]
    public void Announcement_Should_HaveValidLength()
    {
        //TODO Arrange
        var announcement = new Announcement()
        {
            Content = new string('x', 251)
        };
        
        //TODO Act
        var context = new ValidationContext(announcement);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(announcement, context, results, true);

        //TODO Assert
        Assert.False(isValid);
    }

    [Fact]
    public void Announcement_Should_BeValid_With_ValidContent()
    {
        //TODO Arrange
        var announcement = new Announcement()
        {
            Id = 1,
            Content = "Salut baieti",
            Date = DateTime.Now,
            Team = new Team(),
            TeamId = 1
        };

        //TODO Act
        var context = new ValidationContext(announcement);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(announcement, context, results, true);
        
        //TODO Assert
        Assert.True(isValid);
    }
}