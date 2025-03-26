using BackendAPI.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace BackendAPI.Services
{
    /// <summary>
    /// Implementación del servicio de clima.
    /// </summary>
    public class WeatherService : IWeatherService
    {
        private readonly Microsoft.Extensions.Logging.ILogger<WeatherService> _logger;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="WeatherService"/>.
        /// </summary>
        /// <param name="logger">El servicio de logging.</param>
        public WeatherService(Microsoft.Extensions.Logging.ILogger<WeatherService> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public IEnumerable<WeatherForecast> GetWeatherForecast()
        {
            _logger.LogInformation("Generando el pronóstico del clima.");

            var forecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = "Sunny"
            });

            _logger.LogInformation("Pronóstico generado con {Count} elementos.", forecast.Count());

            return forecast;
        }
    }
}