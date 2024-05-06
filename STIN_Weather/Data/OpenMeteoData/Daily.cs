namespace STIN_Weather.Data.OpenMeteoData;

public class Daily
{
    public string[] time { get; set; }
    public double[] temperature_2m_max { get; set; }
    public int[] weather_code { get; set; }
    public double[] precipitation_sum { get; set; }
}
