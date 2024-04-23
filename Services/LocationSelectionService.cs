using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using STIN_Weather.Data;
namespace STIN_Weather.Services;

public class LocationSelectionService
{
    List<SavedLocation> SavedLocations { get; set; }
    private readonly AuthenticationStateProvider AuthenticationStateProvider;
    private readonly UserManager<ApplicationUser> UserManager;
    
    private LocationSelectionService(AuthenticationStateProvider AuthenticationStateProvider, UserManager<ApplicationUser> UserManager,List<SavedLocation> savedLocations)
    {
        this.AuthenticationStateProvider = AuthenticationStateProvider;
        this.UserManager = UserManager;
        this.SavedLocations = savedLocations;
    }

    public  static async Task<LocationSelectionService> BuildLocationSelectionService(AuthenticationStateProvider AuthenticationStateProvider, UserManager<ApplicationUser> UserManager)
    {
        var savedLocations = await GetLocations(AuthenticationStateProvider, UserManager);
        return new LocationSelectionService(AuthenticationStateProvider, UserManager,savedLocations);
    }

    private static async Task<List<SavedLocation>> GetLocations(AuthenticationStateProvider AuthenticationStateProvider, UserManager<ApplicationUser> UserManager)
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        ApplicationUser user = await UserManager.GetUserAsync(authState.User);
        return user.savedLocations;
    }

    public async Task<string> FromId(int id)
    {
        if (id < 1)
        {
            throw new ArgumentException("Invalid ID");
        }

        var ids = SavedLocations.Select(x => x.id);

        if (!ids.Contains(id))
        {
            throw new ArgumentException("Selected ID does not corespond to any location");
        }

        var location = SavedLocations[id - 1];
        var forecastService=new WeatherForecastService(location.latitude,location.longitude,false);
        return await forecastService.GetForecastAsync();

    }
}
