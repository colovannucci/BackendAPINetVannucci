using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Data
{
    /// <summary>
    /// Database context for the application.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AppDbContext"/>.
        /// </summary>
        /// <param name="options">The options to configure the database context.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// DbSet for books.
        /// </summary>
        public DbSet<Book> Books { get; set; }

        /// <summary>
        /// DbSet for authors.
        /// </summary>
        public DbSet<Author> Authors { get; set; }

        /// <summary>
        /// DbSet for genres.
        /// </summary>
        public DbSet<Genre> Genres { get; set; }

        /// <summary>
        /// DbSet for users.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Additional configuration for PostgreSQL and entities.
        /// </summary>
        /// <param name="modelBuilder">The model builder used to configure the entities.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // PostgreSQL-specific configuration
            modelBuilder.HasPostgresExtension("uuid-ossp"); // Enable PostgreSQL extensions (optional)

            // Book entity configuration
            modelBuilder.Entity<Book>()
                .HasIndex(b => b.ISBN)
                .IsUnique(); // ISBN must be unique

            modelBuilder.Entity<Book>()
                .Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);

            // Author entity configuration
            modelBuilder.Entity<Author>()
                .Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Genre entity configuration
            modelBuilder.Entity<Genre>()
                .Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(50);

            // User entity configuration
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique(); // Email must be unique
        }
    }
}