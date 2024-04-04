namespace STIN_Weather.Data
{
    public class WeatherApi
    {
        private static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("https://api.open-meteo.com/v1/forecast"),
        };

        static async Task<string> GetAsync(HttpClient httpClient)
        {
            using HttpResponseMessage response = await httpClient.GetAsync("");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
             
        }
    }
}
