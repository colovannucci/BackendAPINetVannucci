using BackendAPI.Models;
using System.Collections.Generic;

namespace BackendAPI.Data
{
    /// <summary>
    /// Interfaz para acceder a los datos del pronóstico del clima.
    /// </summary>
    public interface IWeatherRepository
    {
        /// <summary>
        /// Obtiene una lista de pronósticos del clima.
        /// </summary>
        /// <returns>Una colección de objetos <see cref="WeatherForecast"/> que representan el pronóstico del clima.</returns>
        IEnumerable<WeatherForecast> GetWeatherForecasts();
    }
}