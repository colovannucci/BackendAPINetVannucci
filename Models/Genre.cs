using System.ComponentModel.DataAnnotations;

namespace BackendAPI.Models
{
    /// <summary>
    /// Represents a genre entity.
    /// </summary>
    public class Genre
    {
        /// <summary>
        /// Gets or sets the unique identifier for the genre.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the genre.
        /// </summary>
        [MaxLength(50)]
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the genre.
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}