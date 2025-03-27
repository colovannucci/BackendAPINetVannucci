using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendAPI.Models
{
    /// <summary>
    /// Represents a book entity.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Gets or sets the unique identifier for the book.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the book.
        /// </summary>
        [MaxLength(200)]
        public required string Title { get; set; }

        /// <summary>
        /// Gets or sets the summary or description of the book.
        /// </summary>
        public required string Summary { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the publication year of the book.
        /// </summary>
        public int PublicationYear { get; set; }

        /// <summary>
        /// Gets or sets the URL of the book's cover image.
        /// </summary>
        public required string ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ISBN of the book. This value must be unique.
        /// </summary>
        [MaxLength(13)]
        public required string ISBN { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the stock quantity of the book.
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the author of the book.
        /// </summary>
        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        /// <summary>
        /// Gets or sets the author of the book.
        /// </summary>
        public Author? Author { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the genre of the book.
        /// </summary>
        [ForeignKey("Genre")]
        public int GenreId { get; set; }

        /// <summary>
        /// Gets or sets the genre of the book.
        /// </summary>
        public Genre? Genre { get; set; }
    }
}