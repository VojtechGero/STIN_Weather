using STIN_Weather.Components;
using STIN_Weather.Components.Pages;
using STIN_Weather.Data;

namespace STIN_Weather.WeatherReportUtils;

public static class WeatherUtils
{
    public static string GetUniqueName(string name,List<SavedLocation> locations)
    {
        int d = 0;
        var names=locations.Select(x => x.name).ToList();
        while (true)
        {
            if (!names.Contains(name)&&d==0) break;
            d++;
            if (!names.Contains(name + $"({d})")) break;

        }
        if (d > 0) return name + $"({d})";
        return name;
    }

    private static string formatDate(DateOnly date, DateOnly today)
    {
        var s = $"{date} ({date.DayOfWeek.ToString().Substring(0, 3)})";
        if (date == today)
        {
            s += " (Today)";
        }
        return s;
    }

    public static async Task<(List<DailyForecast> data, List<string> dates)> CallApi (WeatherApi api, Coordinates c,int historic)
    {
        var builder = new RequestBuilder(c)
            .DailyWeatherCode()
            .DailyTemperatureMax()
            .DailyPrecipitationSum();
        if (historic > 0)
        {
            builder.HistoricDays(historic);
        }
        var dates = new List<string>();
        var data = await api.requestWeather(builder.GetRequest());
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        foreach (var i in data.Select(x => x.date))
        {
            string s = formatDate(i, today);
            dates.Add(s);
        }
        return (data, dates);
    }

    public static (Coordinates,int,bool) ShowWeatherTable(Coordinates coords,bool useHistoric)
    {
        int historic;
        if (useHistoric)
        {
            historic = 7;
        }
        else historic = 0;
        return (coords, historic,true);
    }

    public static string ParseCode(int weatherCode)
    {
        return weatherCode switch
        {
            0 => "Clear sky",
            1 => "Mainly clear",
            2 => "Partly cloudy",
            3 => "Overcast",
            45 => "Fog",
            48 => "Depositing rime fog",
            51 => "Light drizzle",
            53 => "Moderate drizzle",
            55 => "Dense drizzle",
            56 => "Light freezing Drizzle",
            57 => "Dense Freezing Drizzle",
            61 => "Slight rain",
            63 => "Moderate Rain",
            65 => "Heavy rain",
            66 => "Light freezing Rain",
            67 => "Heavy Freezing Rain",
            71 => "Slight snow fall",
            73 => "Moderate snow fall",
            75 => "Heavy snow fall",
            77 => "Snow grains",
            80 => "Slight rain showers",
            81 => "Moderate rain showers",
            82 => "Violent rain showers",
            85 => "Slight snow showers",
            86 => "Heavy snow showers",
            95 => "Thunderstorm",
            96 => "Thunderstorm with slight hail",
            99 => "Thunderstorm with heavy hail",
            _ => "Unknown"
        };
    }
}