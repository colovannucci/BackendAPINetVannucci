using BackendAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    /// <summary>
    /// Controlador para manejar la autenticación de usuarios.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="AuthController"/>.
        /// </summary>
        /// <param name="authService">El servicio de autenticación utilizado para validar credenciales y generar tokens JWT.</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Autentica al usuario y genera un token JWT si las credenciales son válidas.
        /// </summary>
        /// <param name="request">El objeto que contiene el nombre de usuario y la contraseña.</param>
        /// <returns>
        /// Un token JWT si las credenciales son válidas; de lo contrario, un código de estado HTTP 401.
        /// </returns>
        /// <response code="200">Devuelve el token JWT generado.</response>
        /// <response code="401">Las credenciales proporcionadas son inválidas.</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var token = _authService.Authenticate(request.Username, request.Password);

            if (string.IsNullOrWhiteSpace(token))
            {
                return Unauthorized("Credenciales inválidas.");
            }

            return Ok(new { token });
        }
    }

    /// <summary>
    /// Modelo para la solicitud de inicio de sesión.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// El nombre de usuario del cliente.
        /// </summary>
        public required string Username { get; set; }

        /// <summary>
        /// La contraseña del cliente.
        /// </summary>
        public required string Password { get; set; }
    }
}