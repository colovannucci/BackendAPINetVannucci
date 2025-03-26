using BackendAPI.Services;
using BackendAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BackendAPI.Controllers
{
    /// <summary>
    /// Controlador para manejar las solicitudes relacionadas con el clima.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        private readonly Microsoft.Extensions.Logging.ILogger<WeatherController> _logger;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="WeatherController"/>.
        /// </summary>
        /// <param name="weatherService">El servicio de clima.</param>
        /// <param name="logger">El servicio de logging.</param>
        public WeatherController(IWeatherService weatherService, Microsoft.Extensions.Logging.ILogger<WeatherController> logger)
        {
            _weatherService = weatherService;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el pronóstico del clima para los próximos días.
        /// </summary>
        /// <returns>Una lista de objetos <see cref="WeatherForecast"/>.</returns>
        /// Devuelve un código de estado HTTP 400 si el parámetro es inválido, 204 si no hay datos del clima, o 500 si ocurre un error interno.
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<WeatherForecast>> GetWeatherForecast()
        {
            try
            {
                _logger.LogInformation("Se recibió una solicitud para obtener el pronóstico del clima.");

                var forecast = _weatherService.GetWeatherForecast();

                if (forecast == null || !forecast.Any())
                {
                    _logger.LogInformation("No se encontró ningún pronóstico del clima.");
                    return NoContent();
                }

                _logger.LogInformation("Se devolverá el pronóstico del clima con {Count} elementos.", forecast.Count());
                return Ok(forecast);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al obtener el pronóstico del clima.");
                return StatusCode(500, "Ocurrió un error interno.");
            }
        }

        /// <summary>
        /// Obtiene el pronóstico del clima para una ciudad específica.
        /// </summary>
        /// <param name="city">El nombre de la ciudad para la cual se solicita el pronóstico.</param>
        /// <returns>
        /// Una lista de objetos <see cref="WeatherForecast"/> si hay datos disponibles.
        /// Devuelve un código de estado HTTP 400 si el parámetro es inválido, 404 si no hay datos para la ciudad, o 500 si ocurre un error interno.
        /// </returns>
        [HttpGet("{city}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<WeatherForecast>> GetWeatherByCity(string city)
        {
            try
            {
                _logger.LogInformation("Se recibió una solicitud para obtener el pronóstico del clima para la ciudad: {City}.", city);

                // Validar el parámetro de la ciudad
                if (string.IsNullOrWhiteSpace(city))
                {
                    _logger.LogWarning("El parámetro de la ciudad es inválido o no puede estar vacío.");
                    return BadRequest("El parámetro de la ciudad es inválido o no puede estar vacío.");
                }

                // Obtener el pronóstico para la ciudad
                var forecast = _weatherService.GetWeatherByCity(city);

                // Si no hay datos para la ciudad, devolver 404
                if (forecast == null || !forecast.Any())
                {
                    _logger.LogWarning("No se encontraron datos para la ciudad: {City}.", city);
                    return NotFound($"No se encontraron datos para la ciudad: {city}.");
                }

                _logger.LogInformation("Se devolverá el pronóstico del clima para la ciudad: {City} con {Count} elementos.", city, forecast.Count());

                return Ok(forecast);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al obtener el pronóstico del clima para la ciudad: {City}.", city);
                return StatusCode(500, "Ocurrió un error interno.");
            }
        }
    }
}