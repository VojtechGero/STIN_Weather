using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using STIN_Weather.Data;
using STIN_Weather.Endpoints;
using STIN_Weather.Services;
using STIN_Weather.WeatherReportUtils;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Tests;
[TestClass]
public class WeatherControllerTests
{
    private Mock<UserManager<ApplicationUser>> userManagerMock;
    private WeatherController controller;
    private Mock<WeatherApi> weatherApiMock;
    private Mock<IUserStore<ApplicationUser>> userStoreMock = new Mock<IUserStore<ApplicationUser>>();

    [TestInitialize]
    public void Setup()
    {
        userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
        weatherApiMock = new Mock<WeatherApi>();
        var dummyData = new List<DailyForecast>
        {
            new DailyForecast
            {
                Description = "Rainy",
                Date = DateOnly.FromDateTime(DateTime.Today),
                TemperatureMax = 22,
                PrecipitationSum = 5.4
            }
        };
        weatherApiMock.Setup(x => x.requestWeather(It.IsAny<string>()).Result).Returns(dummyData);
        controller = new WeatherController(userManagerMock.Object, weatherApiMock.Object);

        // Setup the HttpContext to simulate User.Identity
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "user1")
            };
        var identity = new ClaimsIdentity(claims, "TestAuthType");
        var claimsPrincipal = new ClaimsPrincipal(identity);
        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = claimsPrincipal }
        };
    }

    [TestMethod]
    public async Task GetForecast_ValidRequest_ReturnsContent()
    {
        // Arrange
        var applicationUser = new ApplicationUser
        {
            savedLocations = new List<SavedLocation> {
                    new SavedLocation { id = 1, latitude = 50.0, longitude = 14.0 }
                }
        };
        userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(applicationUser);

        // Act
        var result = await controller.Get(1, true) as ContentResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Content.Contains("Rainy"));  // Assuming the service responds with "temperature" in the serialized JSON
    }

    [TestMethod]
    public async Task GetForecast_UnknownId_ReturnsBadRequestWithErrorMessage()
    {
        // Arrange
        var applicationUser = new ApplicationUser
        {
            savedLocations = new List<SavedLocation>() // Empty list or no match for provided ID
        };
        userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(applicationUser);

        // Act
        var result = await controller.Get(999, false) as BadRequestObjectResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        Assert.AreEqual("Selected ID does not correspond to any location", result.Value);
    }
    [TestMethod]
    public async Task GetForecast_InvalidId_ReturnsBadRequestWithErrorMessage()
    {
        // Arrange
        var applicationUser = new ApplicationUser
        {
            savedLocations = new List<SavedLocation>() // Empty list or no match for provided ID
        };
        userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(applicationUser);

        // Act
        var result = await controller.Get(-10, false) as BadRequestObjectResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        Assert.AreEqual("Invalid ID", result.Value);
    }
}
