using System.Text.Json;
using System.Text.Json.Serialization;
using STIN_Weather.Data.OpenMeteoData;

namespace STIN_Weather.Data;

public class WeatherData
{
    public double latitude { get; set; }
    public double longitude { get; set; }
    public Daily daily { get; set; }

}

