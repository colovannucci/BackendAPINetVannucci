using BackendAPI.Models;
using System.Collections.Generic;

namespace BackendAPI.Data
{
    /// <summary>
    /// Repositorio para acceder a los datos del pronóstico del clima.
    /// </summary>
    public class WeatherRepository : IWeatherRepository
    {
        /// <summary>
        /// Obtiene una lista de pronósticos del clima.
        /// </summary>
        /// <remarks>
        /// Actualmente, los datos son simulados. En un entorno real, este método debería obtener los datos desde una base de datos o un servicio externo.
        /// </remarks>
        /// <returns>Una colección de objetos <see cref="WeatherForecast"/> que representan el pronóstico del clima.</returns>
        public IEnumerable<WeatherForecast> GetWeatherForecasts()
        {
            // TODO: Obtener datos de servicio externo.
            // Simulación de datos obtenidos
            return new List<WeatherForecast>
            {
                new WeatherForecast { Date = DateTime.Now.AddDays(1), TemperatureC = 25, Summary = "Sunny" },
                new WeatherForecast { Date = DateTime.Now.AddDays(2), TemperatureC = 18, Summary = "Cloudy" },
                new WeatherForecast { Date = DateTime.Now.AddDays(3), TemperatureC = 10, Summary = "Rainy" },
                new WeatherForecast { Date = DateTime.Now.AddDays(4), TemperatureC = 30, Summary = "Hot" },
                new WeatherForecast { Date = DateTime.Now.AddDays(5), TemperatureC = -5, Summary = "Snowy" }
            };
        }
    }
}