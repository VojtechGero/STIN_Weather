using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using STIN_Weather.Data;
namespace STIN_Weather.Services;

public class LocationSelectionService
{
    List<SavedLocation> SavedLocations { get; set; }
    private readonly UserManager<ApplicationUser> UserManager;
    
    private LocationSelectionService(List<SavedLocation> savedLocations)
    {
        this.SavedLocations = savedLocations;
    }

    public static async Task<LocationSelectionService> BuildLocationSelectionService(ApplicationUser user)
    {
        return new LocationSelectionService(user.savedLocations);
    }

    public async Task<string> FromId(int id,bool historic)
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
        var forecastService=new WeatherForecastService(location.latitude,location.longitude, historic);
        return await forecastService.GetForecastAsync();

    }
}
