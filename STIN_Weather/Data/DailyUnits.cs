namespace STIN_Weather.Data
{
    public class DailyUnits
    {
        public string time { get; set; }
        public string temperature_2m_max { get; set; }
        public string weather_code { get; set; }
        public string precipitation_sum { get; set; }

        public DailyUnits() { }
    }
}
