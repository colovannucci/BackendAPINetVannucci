using System.ComponentModel.DataAnnotations;

namespace BackendAPI.Models
{
    /// <summary>
    /// Represents a user entity.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        [MaxLength(100)]
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the email of the user. This value must be unique.
        /// </summary>
        [EmailAddress]
        [MaxLength(100)]
        public required string Email { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string Password { get; set; } = "GeneratedPassword";

        /// <summary>
        /// Gets or sets the status of the user (e.g., Active or Inactive).
        /// </summary>
        [MaxLength(10)]
        public string Status { get; set; } = "Active";
    }
}