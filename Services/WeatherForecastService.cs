using STIN_Weather.WeatherReportUtils;
using STIN_Weather.Data;
using System.Text.Json;

namespace STIN_Weather.Services;

public class WeatherForecastService
{
    Coordinates coords;
    bool useHistoric;
    WeatherApi api;
    public WeatherForecastService(double latitude,double longitude,bool useHistoric)
    {
        this.coords=new Coordinates(latitude,longitude);
        this.useHistoric=useHistoric;
        api = new WeatherApi();
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
        var data = await api.requestWeather(request);
        List<OutgoingForecast> outgoingForecasts = new List<OutgoingForecast>();
        var daily = data.daily;
        for (int i=0;i<daily.time.Length;i++)
        {
            DateOnly date = DateOnly.FromDateTime(DateTime.Parse(daily.time[i]));
            string description = WeatherUtils.ParseCode(daily.weather_code[i]);
            outgoingForecasts.Add(new OutgoingForecast(description, date,
                daily.temperature_2m_max[i], daily.precipitation_sum[i]));
        }
        return JsonSerializer.Serialize(outgoingForecasts);
    }

}
