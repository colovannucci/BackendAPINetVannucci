using BackendAPI.Services;
using BackendAPI.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BackendAPI.Configurations
{
    /// <summary>
    /// Configuración para la inyección de dependencias.
    /// </summary>
    public static class DependencyInjectionConfig
    {
        /// <summary>
        /// Registra los servicios y repositorios en el contenedor de dependencias.
        /// </summary>
        /// <param name="services">El contenedor de servicios.</param>
        public static void RegisterServices(IServiceCollection services)
        {
            // Register services
            services.AddScoped<IWeatherService, WeatherService>();

            // Register repositories
            services.AddScoped<IWeatherRepository, WeatherRepository>();
        }
    }
}