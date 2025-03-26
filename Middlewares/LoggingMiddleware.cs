using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BackendAPI.Middlewares
{
    /// <summary>
    /// Middleware para registrar códigos de estado HTTP específicos (401 y 403) en el sistema de logs.
    /// </summary>
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="LoggingMiddleware"/>.
        /// </summary>
        /// <param name="next">El siguiente middleware en la tubería de solicitudes.</param>
        /// <param name="logger">El servicio de logging.</param>
        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invoca el middleware para procesar la solicitud HTTP.
        /// </summary>
        /// <param name="context">El contexto HTTP actual.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            // Registrar códigos 401 y 403
            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                _logger.LogWarning("Se devolvió un código 401 Unauthorized para la ruta {Path}.", context.Request.Path);
            }
            else if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
            {
                _logger.LogWarning("Se devolvió un código 403 Forbidden para la ruta {Path}.", context.Request.Path);
            }
        }
    }
}