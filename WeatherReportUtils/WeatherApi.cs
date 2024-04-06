using System.Text.Json;
using System.Text.Json.Serialization;
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

    public async Task<WeatherData> requestWeather(string request)
    {
        try
        {
            string json = await GetAsync(request);
            var we = JsonSerializer.Deserialize<WeatherData>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return we;
        }
        catch(HttpRequestException)
        {
            throw;
        }
    }
}
