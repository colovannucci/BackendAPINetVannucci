using BackendAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace BackendAPI.Services
{
    /// <summary>
    /// Implementación del servicio de clima.
    /// </summary>
    public class WeatherService : IWeatherService
    {
        /// <inheritdoc />
        public IEnumerable<WeatherForecast> GetWeatherForecast()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = "Sunny"
            });
        }
    }
}