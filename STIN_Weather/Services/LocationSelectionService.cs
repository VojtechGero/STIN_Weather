using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using STIN_Weather.Data;
using STIN_Weather.WeatherReportUtils;
namespace STIN_Weather.Services;

public class LocationSelectionService
{
    List<SavedLocation> SavedLocations { get; set; }
    private readonly UserManager<ApplicationUser> UserManager;
    private readonly WeatherApi api;
    
    public LocationSelectionService(ApplicationUser user,WeatherApi api)
    {
        SavedLocations = user.savedLocations;
        this.api= api;
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
            throw new ArgumentException("Selected ID does not correspond to any location");
        }

        var location = SavedLocations[id - 1];
        var forecastService=new WeatherForecastService(api,location.latitude,location.longitude, historic);
        return await forecastService.GetForecastAsync();

    }
}
