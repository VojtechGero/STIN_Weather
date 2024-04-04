namespace STIN_Weather.Data;
public class Weather
{
    public string json {  get; set; }

    async Weather(double latitude,double longitude, WeatherApi w)
    {
        json=w.GetAsync()
    }

    public async Weather requestWeather(Coordinates c,WeatherApi w)
    {
        if (c.latitude is null or c.longitude is null) return null;
        return await new Weather(c.latitude, c.longitude, WeatherApi w);
    }
}
