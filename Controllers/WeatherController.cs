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
    }
}