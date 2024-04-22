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
            73 => "Mmoderate snow fall",
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
            _ => ""
        };
    }
}