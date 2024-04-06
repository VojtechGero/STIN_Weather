using STIN_Weather.Data;

namespace STIN_Weather.WeatherReportUtils;

public class RequestBuilder
{
    private string request;
    public RequestBuilder(Coordinates coords)
    {
        request = $"?latitude={coords.latitude}&longitude={coords.longitude}";
    }
    public void DailyTemperatureMax()
    {
        request += "&daily=temperature_2m_max";
    }
    public void DailyWeatherCode()
    {
        request += "&daily=weather_code";
    }
    public void DailyPrecipitationSum()
    {
        request += "&daily=precipitation_sum";
    }
    public string GetRequest()
    {
        return request;
    }
}
