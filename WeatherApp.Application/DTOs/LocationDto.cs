namespace WeatherApp.Application.DTOs
{
    public class LocationDto
    {
        public int Id { get; set; }
        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }

    public class CreateLocationDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class LocationCreatedResponse
    {
        public int Id { get; set; }
    }
}
