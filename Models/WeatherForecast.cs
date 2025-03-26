namespace BackendAPI.Models
{
    /// <summary>
    /// Representa el pronóstico del clima para un día específico.
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// Fecha del pronóstico.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Temperatura en grados Celsius.
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// Descripción del clima (por ejemplo, soleado, lluvioso).
        /// </summary>
        public string Summary { get; set; } = string.Empty;
    }
}