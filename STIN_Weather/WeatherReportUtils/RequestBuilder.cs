using STIN_Weather.Data;

namespace STIN_Weather.WeatherReportUtils;

public class RequestBuilder
{
    private string request;
    public RequestBuilder(Coordinates coords)
    {
        request = $"?latitude={coords.latitude}&longitude={coords.longitude}";
    }
    public RequestBuilder DailyTemperatureMax()
    {
        request += "&daily=temperature_2m_max";
        return this;
    }
    public RequestBuilder DailyWeatherCode()
    {
        request += "&daily=weather_code";
        return this;
    }
    public RequestBuilder DailyPrecipitationSum()
    {
        request += "&daily=precipitation_sum";
        return this;
    }

    public RequestBuilder HistoricDays(int days)
    {
        if (days < 0) throw new ArgumentOutOfRangeException();
        request += $"&past_days={days}";
        return this;
    }

    public string GetRequest()
    {
        return request;
    }
}
