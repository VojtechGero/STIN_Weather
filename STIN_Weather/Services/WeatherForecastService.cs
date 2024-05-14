using STIN_Weather.WeatherReportUtils;
using STIN_Weather.Data;
using System.Text.Json;

namespace STIN_Weather.Services;

public class WeatherForecastService
{
    Coordinates coords;
    bool useHistoric;
    WeatherApi api;
    public WeatherForecastService(WeatherApi api,double latitude,double longitude,bool useHistoric)
    {
        this.coords=new Coordinates(latitude,longitude);
        this.useHistoric=useHistoric;
        this.api=api;
    }

    private string BuildRequest()
    {
        var builder = new RequestBuilder(coords);
        builder.DailyTemperatureMax()
            .DailyPrecipitationSum()
            .DailyWeatherCode();
        if(useHistoric)
        {
            builder.HistoricDays(7);
        }
        return builder.GetRequest();
    }

    public async Task<string> GetForecastAsync()
    {
        string request = BuildRequest();
        var forecasts = await api.requestWeather(request);
        return JsonSerializer.Serialize(forecasts);
    }

}
