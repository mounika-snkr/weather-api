namespace WeatherApp.Application.DTOs
{
    public class ForecastDto
    {
        public int Id { get; set; }

        public int LocationId { get; set; }
        public double? Temperature { get; set; }

        public double? WindSpeed { get; set; }
    }
}
