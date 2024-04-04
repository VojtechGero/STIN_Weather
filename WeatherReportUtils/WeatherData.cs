using STIN_Weather.Data;

namespace STIN_Weather.WeatherReportUtils
{
    public class WeatherData
    {
        public string json { get; set; }
        double latitude, longitude;
        WeatherData(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public static async Task<WeatherData> requestWeather(Coordinates c, WeatherApi w)
        {
            if (c.latitude is null || c.longitude is null) return null;
            var we = new WeatherData((double)c.latitude, (double)c.longitude);
            we.json = await w.GetAsync(we.latitude, we.longitude);
            return we;
        }
    }
}
