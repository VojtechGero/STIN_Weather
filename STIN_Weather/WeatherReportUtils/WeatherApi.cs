using System.Text.Json;
using STIN_Weather.Data;

namespace STIN_Weather.WeatherReportUtils;

public class WeatherApi
{
    private HttpClient client = new()
    {
        BaseAddress = new Uri("https://api.open-meteo.com/v1/forecast"),
    };

    private async Task<string> GetAsync(string request)
    {
        using HttpResponseMessage response = await client.GetAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async virtual Task<List<DailyForecast>> requestWeather(string request)
    {
        try
        {
            string json = await GetAsync(request);
            var we = JsonSerializer.Deserialize<WeatherData>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            List<DailyForecast> Forecast = new List<DailyForecast>();
            var daily = we.daily;
            for (int i = 0; i < daily.time.Length; i++)
            {
                DateOnly date = DateOnly.FromDateTime(DateTime.Parse(daily.time[i]));
                string description = WeatherUtils.ParseCode(daily.weather_code[i]);
                Forecast.Add(new DailyForecast(description, date,
                    daily.temperature_2m_max[i], daily.precipitation_sum[i]));
            }
            return Forecast;
        }
        catch(HttpRequestException)
        {
            throw;
        }
    }
}
