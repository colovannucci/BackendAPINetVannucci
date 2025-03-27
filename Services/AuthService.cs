using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackendAPI.Services
{
    /// <summary>
    /// Servicio para manejar la autenticación y generación de tokens JWT.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="AuthService"/>.
        /// </summary>
        /// <param name="configuration">La configuración de la aplicación para obtener los valores de JWT.</param>
        public AuthService(IConfiguration configuration)
        {
            // Validar que los valores no sean nulos o vacíos
            _key = configuration["Jwt:Key"] ?? throw new InvalidOperationException("La clave JWT no está configurada.");
            _issuer = configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("El emisor JWT no está configurado.");
            _audience = configuration["Jwt:Audience"] ?? throw new InvalidOperationException("La audiencia JWT no está configurada.");
        }

        /// <summary>
        /// Autentica al usuario y genera un token JWT si las credenciales son válidas.
        /// </summary>
        /// <param name="username">El nombre de usuario.</param>
        /// <param name="password">La contraseña del usuario.</param>
        /// <returns>
        /// Un token JWT si las credenciales son válidas; de lo contrario, <c>null</c>.
        /// </returns>
        public string? Authenticate(string username, string password)
        {
            // TODO: Usar una base de datos en producción.
            // Simular datos para el admin
            var dbUsername = "admin";
            var dbPassword = "admin";
            
            // Validar credenciales
            if (username != dbUsername || password != dbPassword)
            {
                return null; // Credenciales inválidas
            }

            // Generar el token JWT
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}