using System.Text.Json.Serialization;

namespace STIN_Weather.Data;

public class DailyForecast
{
    public string Description {get; set;}
    public DateOnly Date { get; set; }
    public double TemperatureMax { get; set; }
    public double PrecipitationSum { get; set; }

    [JsonIgnore]
    public string ImageLink { get; set; }
}
