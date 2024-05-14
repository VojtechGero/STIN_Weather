using STIN_Weather.Components;
using STIN_Weather.Components.Pages;
using STIN_Weather.Data;

namespace STIN_Weather.WeatherReportUtils;

public static class WeatherUtils
{
    public static string GetUniqueName(string name, List<SavedLocation> locations)
    {
        int d = 0;
        if (string.IsNullOrWhiteSpace(name))
        {
            name = "New location";
        }
        var names = locations.Select(x => x.name).ToList();
        while (true)
        {
            if (!names.Contains(name) && d == 0) break;
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

    public static async Task<(List<DailyForecast> data, List<string> dates)> CallApi(WeatherApi api, Coordinates c, int historic)
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
        foreach (var i in data.Select(x => x.Date))
        {
            string s = formatDate(i, today);
            dates.Add(s);
        }
        return (data, dates);
    }

    public static List<SavedLocation> removeLocation(int id, List<SavedLocation> locations)
    {
        if (id > locations.Count) return locations;
        locations.RemoveAt(id - 1);
        for (int i = id - 1; i < locations.Count; i++)
        {
            locations[i].id = i + 1;
        }

        return locations;
    }
    public static (Coordinates, int, bool) ShowWeatherTable(Coordinates coords, bool useHistoric)
    {
        int historic;
        if (useHistoric)
        {
            historic = 7;
        }
        else historic = 0;
        return (coords, historic, true);
    }


    public static string GetImage(int weatherCode)
    {
        return weatherCode switch
        {
            0 => "http://openweathermap.org/img/wn/01d@2x.png",
            1 => "http://openweathermap.org/img/wn/01d@2x.png",
            2 => "http://openweathermap.org/img/wn/02d@2x.png",
            3 => "http://openweathermap.org/img/wn/03d@2x.png",
            45 => "http://openweathermap.org/img/wn/50d@2x.png",
            48 => "http://openweathermap.org/img/wn/50d@2x.png",
            51 => "http://openweathermap.org/img/wn/09d@2x.png",
            53 => "http://openweathermap.org/img/wn/09d@2x.png",
            55 => "http://openweathermap.org/img/wn/09d@2x.png",
            56 => "http://openweathermap.org/img/wn/09d@2x.png",
            57 => "http://openweathermap.org/img/wn/09d@2x.png",
            61 => "http://openweathermap.org/img/wn/10d@2x.png",
            63 => "http://openweathermap.org/img/wn/10d@2x.png",
            65 => "http://openweathermap.org/img/wn/10d@2x.png",
            66 => "http://openweathermap.org/img/wn/10d@2x.png",
            67 => "http://openweathermap.org/img/wn/10d@2x.png",
            71 => "http://openweathermap.org/img/wn/13d@2x.png",
            73 => "http://openweathermap.org/img/wn/13d@2x.png",
            75 => "http://openweathermap.org/img/wn/13d@2x.png",
            77 => "http://openweathermap.org/img/wn/13d@2x.png",
            80 => "http://openweathermap.org/img/wn/09d@2x.png",
            81 => "http://openweathermap.org/img/wn/09d@2x.png",
            82 => "http://openweathermap.org/img/wn/09d@2x.png",
            85 => "http://openweathermap.org/img/wn/13d@2x.png",
            86 => "http://openweathermap.org/img/wn/13d@2x.png",
            95 => "http://openweathermap.org/img/wn/11d@2x.png",
            96 => "http://openweathermap.org/img/wn/11d@2x.png",
            99 => "http://openweathermap.org/img/wn/11d@2x.png"
        };
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