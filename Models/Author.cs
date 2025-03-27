using System.ComponentModel.DataAnnotations;

namespace BackendAPI.Models
{
    /// <summary>
    /// Represents an author entity.
    /// </summary>
    public class Author
    {
        /// <summary>
        /// Gets or sets the unique identifier for the author.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the author.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the nationality of the author.
        /// </summary>
        public string Nationality { get; set; } = "Uruguay";

        /// <summary>
        /// Gets or sets the birth date of the author.
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the collection of books written by the author.
        /// </summary>
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}