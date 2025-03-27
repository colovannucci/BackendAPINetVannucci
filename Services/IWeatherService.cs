using BackendAPI.Models;
using System.Collections.Generic;

namespace BackendAPI.Services
{
    /// <summary>
    /// Define los métodos para manejar la lógica de negocio relacionada con el clima.
    /// </summary>
    public interface IWeatherService
    {
        /// <summary>
        /// Obtiene el pronóstico del clima.
        /// </summary>
        /// <returns>Una lista de objetos <see cref="WeatherForecast"/>.</returns>
        IEnumerable<WeatherForecast> GetWeatherForecast();

        /// <summary>
        /// Obtiene el pronóstico del clima para una ciudad específica.
        /// </summary>
        /// <param name="city">El nombre de la ciudad.</param>
        /// <returns>Una lista de objetos <see cref="WeatherForecast"/> para la ciudad especificada.</returns>
        IEnumerable<WeatherForecast> GetWeatherByCity(string city);
    }
}