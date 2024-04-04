namespace STIN_Weather.Data
{
    public class Weather
    {
        public string json {  get; set; }

        Weather(double latitude, double longitude)
        {

        }

        public Weather requestWeather(double? latitude, double? longitude)
        {
            if (latitude is null or longitude is null) return null;
            return new Weather(latitude, longitude);
        }
    }
}
