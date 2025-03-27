using BackendAPI.Models;
using BackendAPI.Data;
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
        private readonly IWeatherRepository _weatherRepository;
        private readonly ILogger<WeatherService> _logger;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="WeatherService"/>.
        /// </summary>
        /// <param name="weatherRepository">El repositorio de datos del clima.</param>
        /// <param name="logger">El servicio de logging.</param>
        public WeatherService(IWeatherRepository weatherRepository, ILogger<WeatherService> logger)
        {
            _weatherRepository = weatherRepository;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el pronóstico del clima para los próximos días.
        /// </summary>
        /// <remarks>
        /// Este método utiliza la capa de datos para obtener el pronóstico del clima.
        /// </remarks>
        /// <returns>
        /// Una colección de objetos <see cref="WeatherForecast"/> que representan el pronóstico del clima.
        /// </returns>
        public IEnumerable<WeatherForecast> GetWeatherForecast()
        {
            _logger.LogInformation("Obteniendo el pronóstico del clima desde la capa de datos.");

            var forecast = _weatherRepository.GetWeatherForecasts();

            _logger.LogInformation("Pronóstico obtenido con {Count} elementos.", forecast.Count());

            return forecast;
        }

        /// <summary>
        /// Obtiene el pronóstico del clima para una ciudad específica.
        /// </summary>
        /// <remarks>
        /// Este método verifica si hay datos disponibles para la ciudad solicitada y, si es así, devuelve el pronóstico del clima.
        /// </remarks>
        /// <param name="city">El nombre de la ciudad para la cual se solicita el pronóstico.</param>
        /// <returns>
        /// Una colección de objetos <see cref="WeatherForecast"/> que representan el pronóstico del clima para la ciudad especificada.
        /// Si no hay datos disponibles para la ciudad, se devuelve una colección vacía.
        /// </returns>
        public IEnumerable<WeatherForecast> GetWeatherByCity(string city)
        {
            _logger.LogInformation("Obteniendo el pronóstico del clima para la ciudad: {City}.", city);

            // Simular datos para algunas ciudades (puedes extender la capa de datos para soportar ciudades)
            var citiesWithData = new List<string> { "New York", "London", "Tokyo" };

            if (!citiesWithData.Contains(city, StringComparer.OrdinalIgnoreCase))
            {
                _logger.LogWarning("No hay datos disponibles para la ciudad: {City}.", city);
                return Enumerable.Empty<WeatherForecast>();
            }

            var forecast = _weatherRepository.GetWeatherForecasts();

            _logger.LogInformation("Pronóstico obtenido para la ciudad: {City} con {Count} elementos.", city, forecast.Count());

            return forecast;
        }
    }
}