using System.Security.Claims;
using FakeItEasy;
using FootballMgm.Api.Models;
using FootballMgm.Api.Repositories;
using FootballMgm.Api.Repositories.Announcement;
using FootballMgm.Api.Services.Announcement;
using Microsoft.AspNetCore.Http;
using NSubstitute;

namespace FootballMgm.UnitTests;

public class AnnouncementServiceTests
{
    private readonly AnnouncementService _announcementService;
    private readonly ICoachRepository _fakeCoachRepository;
    private readonly IFootballerRepository _fakeFootballerRepository;
    private readonly IAnnouncementRepository _fakeAnnouncementRepository;

    public AnnouncementServiceTests()
    {
        _fakeCoachRepository = Substitute.For<ICoachRepository>();
        _fakeAnnouncementRepository = Substitute.For<IAnnouncementRepository>();
        _fakeFootballerRepository = Substitute.For<IFootballerRepository>();
        _announcementService = new AnnouncementService(_fakeCoachRepository, _fakeAnnouncementRepository, _fakeFootballerRepository);
    }

    [Fact]
    public void ProcessAnnouncement_WithValidCoach_Should_ReturnSuccess()
    {
        //TODO Arrange
        var httpContext = CreateFakeHttpContext(1);
        var goodCoach = new Coach
        {
            Id = 1,
            TeamId = 1
        };

        _fakeCoachRepository.GetCoachById(1).Returns(goodCoach);
        _fakeAnnouncementRepository.InsertAnnouncement(Arg.Any<Announcement>())
            .Returns((true, "Announcement inserted successfully"));        

        //TODO Act
        var result = _announcementService.ProcessAnnouncement(httpContext, "Test announcement");


        //TODO Assert
        Assert.Equal(200, result.Item1);
    }
    
    [Fact]
    public void ProcessAnnouncement_WithNullCoach_Should_ReturnFailure()
    {
        //TODO Arrange
        var httpContext = CreateFakeHttpContext(1);

        _fakeCoachRepository.GetCoachById(Arg.Any<int>()).Returns((Coach?) null);
        _fakeAnnouncementRepository.InsertAnnouncement(Arg.Any<Announcement>())
            .Returns((true, "Announcement inserted successfully"));        

        //TODO Act
        var result = _announcementService.ProcessAnnouncement(httpContext, "Test announcement");


        //TODO Assert
        Assert.Equal(400, result.Item1);
    }

    [Fact]
    public void ProcessAnnouncement_WithNoTeamCoach_Should_ReturnFailure()
    {
        //TODO Arrange
        var httpContext = CreateFakeHttpContext(1);
        var teamlessCoach = new Coach
        {
            Id = 1
        };
        
        _fakeCoachRepository.GetCoachById(1).Returns(teamlessCoach);
        _fakeAnnouncementRepository.InsertAnnouncement(Arg.Any<Announcement>())
            .Returns((true, "Announcement inserted successfully"));        
        
        //TODO Act
        var result = _announcementService.ProcessAnnouncement(httpContext, "Test announcement");
        
        //TODO Assert
        Assert.NotEqual(200, result.Item1);

    }

    [Fact]
    public void ProcessAnnouncement_WithNoContent_Should_ReturnFailure()
    {
        //TODO Arrange
        var httpContext = CreateFakeHttpContext(1);
        var goodCoach = new Coach()
        {
            Id = 1,
            TeamId = 1
        };
        _fakeCoachRepository.GetCoachById(1).Returns(goodCoach);
        _fakeAnnouncementRepository.InsertAnnouncement(Arg.Any<Announcement>())
            .Returns((true, "Announcement inserted successfully"));        
        
        //TODO Act
        var result = _announcementService.ProcessAnnouncement(httpContext, null);

        //TODO Assert
        Assert.Equal(400, result.Item1);
    }

    [Fact]
    public void ProcessAnnouncement_With_NullUserId_Should_ReturnFailure()
    {
        //TODO Arrange
        var httpContext = CreateFakeHttpContext(0);
        var goodCoach = new Coach()
        {
            Id = 1,
            TeamId = 1
        };
        _fakeCoachRepository.GetCoachById(Arg.Any<int>()).Returns(goodCoach);
        _fakeAnnouncementRepository.InsertAnnouncement(Arg.Any<Announcement>())
            .Returns((true, "Announcement inserted successfully"));
        
        //TODO Act
        var result = _announcementService.ProcessAnnouncement(httpContext, "Salut");

        //TODO Assert
        Assert.Equal(400, result.Item1);
    }


    [Fact]
    public void GetAnnouncements_With_NoCoachOrFootballer_Should_ReturnFailure()
    {
        //TODO Arrange
        var httpContext = CreateFakeHttpContext(1);
        _fakeCoachRepository.GetCoachById(1).Returns((Coach?) null);
        _fakeFootballerRepository.GetFootballerById(1).Returns((Footballer?)null);
        
        //TODO Act
        var result = _announcementService.GetAnnouncements(httpContext);

        //TODO Assert
        Assert.Equal(400, result.Item1);
    }

    [Fact]
    public void GetAnnouncements_With_NullUserId_Should_ReturnFailure()
    {
        //TODO Arrange
        var httpContext = CreateFakeHttpContext(0);
        var goodCoach = new Coach()
        {
            Id = 1,
            TeamId = 1
        };
        _fakeCoachRepository.GetCoachById(Arg.Any<int>()).Returns(goodCoach);
        _fakeFootballerRepository.GetFootballerById(Arg.Any<int>()).Returns((Footballer?)null);
        
        //TODO Act
        var result = _announcementService.GetAnnouncements(httpContext);

        //TODO Assert
        Assert.Equal(400, result.Item1);
    }

    [Fact]
    public void GetAnnouncements_With_ValidCoachProvided_Should_ReturnSuccess()
    {
        //TODO Arrange
        var httpContext = CreateFakeHttpContext(1);
        var goodCoach = new Coach()
        {
            Id = 1,
            TeamId = 1
        };
        _fakeCoachRepository.GetCoachById(Arg.Any<int>()).Returns(goodCoach);
        _fakeFootballerRepository.GetFootballerById(Arg.Any<int>()).Returns((Footballer?)null);
        
        //TODO Act
        var result = _announcementService.GetAnnouncements(httpContext);

        //TODO Assert
        Assert.Equal(200, result.Item1);
    }
    
    [Fact]
    public void GetAnnouncements_With_ValidFootballerProvided_Should_ReturnSuccess()
    {
        //TODO Arrange
        var httpContext = CreateFakeHttpContext(1);
        var goodFootballer = new Footballer()
        {
            Id = 1,
            TeamId = 1
        };
        _fakeCoachRepository.GetCoachById(Arg.Any<int>()).Returns((Coach?) null);
        _fakeFootballerRepository.GetFootballerById(Arg.Any<int>()).Returns(goodFootballer);
        
        //TODO Act
        var result = _announcementService.GetAnnouncements(httpContext);

        //TODO Assert
        Assert.Equal(200, result.Item1);
    }
    
    private static HttpContext CreateFakeHttpContext(int userId)
    {
        var httpContext = Substitute.For<HttpContext>();
        var user = Substitute.For<ClaimsPrincipal>();

        user.FindFirst(ClaimTypes.NameIdentifier)
            .Returns(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));

        httpContext.User.Returns(user);
        
        return httpContext;
    }
}