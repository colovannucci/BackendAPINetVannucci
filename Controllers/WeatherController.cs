using BackendAPI.Services;
using BackendAPI.Models;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="WeatherController"/>.
        /// </summary>
        /// <param name="weatherService">El servicio de clima.</param>
        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        /// <summary>
        /// Obtiene el pronóstico del clima para los próximos días.
        /// </summary>
        /// <returns>Una lista de objetos <see cref="WeatherForecast"/>.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<WeatherForecast>> GetWeatherForecast()
        {
            var forecast = _weatherService.GetWeatherForecast();
            return Ok(forecast);
        }
    }
}