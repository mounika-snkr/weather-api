using Microsoft.EntityFrameworkCore;

namespace WeatherApp.Domain.Entities;

public partial class WeatherAppContext : DbContext
{
    public WeatherAppContext()
    {
    }

    public WeatherAppContext(DbContextOptions<WeatherAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Forecast> Forecasts { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Forecast>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Forecast__3214EC076DE1DC42");

            entity.Property(e => e.FetchedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Location).WithMany(p => p.Forecasts).HasConstraintName("FK__Forecasts__Locat__5FB337D6");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Location__3214EC074A2F33BC");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
