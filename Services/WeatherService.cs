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

        /// <inheritdoc />
        public IEnumerable<WeatherForecast> GetWeatherByCity(string city)
        {
            _logger.LogInformation("Generando el pronóstico del clima para la ciudad: {City}.", city);

            // Simular datos para algunas ciudades
            var citiesWithData = new List<string> { "New York", "London", "Tokyo" };

            if (!citiesWithData.Contains(city, StringComparer.OrdinalIgnoreCase))
            {
                _logger.LogWarning("No hay datos disponibles para la ciudad: {City}.", city);
                return Enumerable.Empty<WeatherForecast>();
            }

            var forecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = "Sunny"
            });

            _logger.LogInformation("Pronóstico generado para la ciudad: {City} con {Count} elementos.", city, forecast.Count());

            return forecast;
        }
    }
}