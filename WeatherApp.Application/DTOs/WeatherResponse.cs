namespace WeatherApp.Application.DTOs
{
     public class WeatherResponse
    {
        public CurrentWeatherDto? current_weather { get; set; }
    }

    public class CurrentWeatherDto
    {
        public string? time { get; set; }
        //public int interval { get; set; }
        //public double? relative_humidity_2m { get; set; }
        public double temperature { get; set; }//default units are Celsius
        public double windspeed { get; set; }
        public int winddirection { get; set; }
        //public int is_day { get; set; }
        public int weathercode { get; set; }
    }
}
