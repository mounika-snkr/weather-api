using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WeatherApp.Domain.Entities;

public partial class Forecast
{
    [Key]
    public int Id { get; set; }

    public int LocationId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ForecastTime { get; set; }

    public double? Temperature { get; set; }

    public double? WindSpeed { get; set; }

    public double? Humidity { get; set; }

    public int? WeatherCode { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FetchedAt { get; set; }

    [ForeignKey("LocationId")]
    [InverseProperty("Forecasts")]
    public virtual Location Location { get; set; } = null!;
}
