namespace BackendAPI.Services
{
    /// <summary>
    /// Define los métodos para manejar la autenticación de usuarios y generación de tokens JWT.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Autentica al usuario y genera un token JWT si las credenciales son válidas.
        /// </summary>
        /// <param name="username">El nombre de usuario del cliente.</param>
        /// <param name="password">La contraseña del cliente.</param>
        /// <returns>
        /// Un token JWT si las credenciales son válidas; de lo contrario, <c>null</c>.
        /// </returns>
        string? Authenticate(string username, string password);
    }
}