using STIN_Weather.WeatherReportUtils;

namespace STIN_Weather.Data
{
    public class WeatherReport
    {
        public string json { get; set; }
        double latitude, longitude;
        WeatherReport(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public async Task<WeatherReport> requestWeather(Coordinates c, WeatherApi w)
        {
            if (c.latitude is null || c.longitude is null) return null;
            var we = new WeatherReport((double)c.latitude, (double)c.longitude);
            we.json = await w.GetAsync(latitude,longitude);
            return we;
        }
    }
}

