namespace STIN_Weather.Data;

public class DailyForecast
{
    public string description {get; set;}
    public DateOnly date { get; set; }
    public double temperatureMax { get; set; }
    public double precipitationSum { get; set; }

    public DailyForecast(string description, DateOnly date, double temperatureMax, double precipitationSum)
    {
        this.description = description;
        this.date = date;
        this.temperatureMax = temperatureMax;
        this.precipitationSum = precipitationSum;
    }
}
