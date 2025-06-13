using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WeatherApp.Domain.Entities;

[Index("Latitude", "Longitude", Name = "UQ_Locations_Lat_Long", IsUnique = true)]
public partial class Location
{
    [Key]
    public int Id { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    [InverseProperty("Location")]
    public virtual ICollection<Forecast> Forecasts { get; set; } = new List<Forecast>();
}
