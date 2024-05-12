using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using STIN_Weather.Components.Pages;
using STIN_Weather.Data;
namespace Tests;

[TestClass]
public class BunitTest :Bunit.TestContext
{
    /*
    [TestMethod]
    public async Task OnInitializedAsync_UserExists_LocationsSet()
    {
        // Arrange
        var user = new ApplicationUser();
        user.savedLocations = new List<SavedLocation> { new SavedLocation(), new SavedLocation() };

        var authState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, "testuser") })));
        var authStateProviderMock = new Mock<AuthenticationStateProvider>();
        authStateProviderMock.Setup(m => m.GetAuthenticationStateAsync()).ReturnsAsync(authState);

        var userManagerMock = new Mock<UserManager<ApplicationUser>>(MockBehavior.Strict);
        userManagerMock.Setup(m => m.GetUserAsync(authState.User)).ReturnsAsync(user);

        var services = new ServiceCollection();
        services.AddSingleton(authStateProviderMock.Object);
        services.AddSingleton(userManagerMock.Object);

        Services.AddSingleton(authStateProviderMock.Object);
        Services.AddSingleton(userManagerMock.Object);
        var cut = Render<Home>();

        // Act

        // Assert
        Assert.AreEqual(2, cut.Instance.locations.Count); // Ensure locations were set
                                                          // Add more assertions as needed
    }
    */
}
