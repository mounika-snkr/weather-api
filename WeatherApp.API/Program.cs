
using Microsoft.EntityFrameworkCore;
using WeatherApp.Application;
using WeatherApp.Application.Interfaces.Locations;
using WeatherApp.Application.Interfaces.Forecasts;
using WeatherApp.Application.Services;
using WeatherApp.Domain.Entities;
using WeatherApp.Infrastructure;
using WeatherApp.Application.Interfaces.OpenMeteo;
using WeatherApp.Infrastructure.Services.OpenMeteo;

namespace WeatherApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region configuration services
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<WeatherAppContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext"),
                    providerOptions => providerOptions.EnableRetryOnFailure());
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddScoped<ILocationService, LocationService>();
            builder.Services.AddScoped<ILocationRepository, LocationRepository>();
            builder.Services.AddScoped<IForecastService, ForecastService>();
            builder.Services.AddScoped<IForecastRepository, ForecastRepository>();
            builder.Services.AddHttpClient<IOpenMeteoService, OpenMeteoService>();

            #endregion

            #region configurating middleware
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

            #endregion
        }
    }
}
