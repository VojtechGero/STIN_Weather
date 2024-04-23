namespace STIN_Weather.Data;

public class OutgoingForecast
{
    public string description {get; set;}
    public DateOnly date { get; set; }
    public double temperatureMax { get; set; }
    public double precipitationSum { get; set; }

    public OutgoingForecast(string description, DateOnly date, double temperatureMax, double precipitationSum)
    {
        this.description = description;
        this.date = date;
        this.temperatureMax = temperatureMax;
        this.precipitationSum = precipitationSum;
    }
}
