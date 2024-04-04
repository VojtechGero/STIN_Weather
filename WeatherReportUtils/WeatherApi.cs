namespace STIN_Weather.WeatherReportUtils
{
    public class WeatherApi
    {
        private HttpClient client = new()
        {
            BaseAddress = new Uri("https://api.open-meteo.com/v1/forecast"),
        };

        public async Task<string> GetAsync(double latitude, double longitude)
        {
            string request = $"?latitude={latitude}&longitude={longitude}";
            using HttpResponseMessage response = await client.GetAsync(request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();

        }
    }
}
