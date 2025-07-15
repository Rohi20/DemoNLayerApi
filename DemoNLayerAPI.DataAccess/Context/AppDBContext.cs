using DemoNLayerApi.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoNLayerApi.Data.Context
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-One User <-> UserProfile
            modelBuilder.Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<UserProfile>(p => p.UserId);

            // One-to-Many Author -> Books
            modelBuilder.Entity<Book>()
                .HasOne(u => u.Author)
                .WithMany(b => b.Books)
                .HasForeignKey(a => a.AuthorId);

            // Many-to-Many Book <-> Category
            modelBuilder.Entity<Category>()
                .HasMany(b => b.Books)
                .WithMany(c => c.Categories)
                .UsingEntity(j => j.ToTable("BookCategory"));

            modelBuilder.Entity<Author>()
                .HasData(new Author
                {
                    Id = 1,
                    Name = "Author 1",
                }, new Author
                {
                    Id = 2,
                    Name = "Author 2",
                },
                new Author
                {
                    Id = 3,
                    Name = "Author 3",
                });

            modelBuilder.Entity<Book>()
                .HasData(new Book
                {
                    Id = 1,
                    Title = "Book A",
                    Description = "Demo description of Book A",
                    AuthorId = 1,
                }, new Book
                {
                    Id = 2,
                    Title = "Book B",
                    Description = "Demo description of Book B",
                    AuthorId = 1,
                }, new Book
                {
                    Id = 3,
                    Title = "Book C",
                    Description = "Demo description of Book C",
                    AuthorId = 2,
                }, new Book
                {
                    Id = 4,
                    Title = "Book D",
                    Description = "Demo description of Book D",
                    AuthorId = 3,
                }, new Book
                {
                    Id = 5,
                    Title = "Book E",
                    Description = "Demo description of Book E",
                    AuthorId = 2,
                }, new Book
                {
                    Id = 6,
                    Title = "Book F",
                    Description = "Demo description of Book F",
                    AuthorId = 1,
                });

            modelBuilder.Entity<Category>()
                .HasData(new Category
                {
                    Id = 1,
                    Name = "Fiction"
                }, new Category
                {
                    Id = 2,
                    Name = "Science"
                });

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Categories)
                .WithMany(c => c.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookCategory",  // the join table name
                    j => j.HasData(
                    new { BooksId = 1, CategoriesId = 1 },  // Book 1 → Category 1
                    new { BooksId = 1, CategoriesId = 2 },  // Book 1 → Category 2
                    new { BooksId = 2, CategoriesId = 1 }   // Book 2 → Category 1
                )
            );

            modelBuilder.Entity<UserProfile>()
                .Property(e => e.Role)
                .HasDefaultValue("Internal");

            modelBuilder.Entity<Book>()
                .Property(e => e.Price)
                .HasDefaultValue(50.00);

            modelBuilder.Entity<Book>()
                .HasData(new Book
                {
                    Id = 10,
                    AuthorId = 5,
                    Title = "Beneath the Crimson Sky",
                    Description = "A sweeping historical romance set during WWII, where love and sacrifice intertwine.",
                    Price = 14.99M
                }, new Book
                {
                    Id = 11,
                    AuthorId = 6,
                    Title = "Digital Shadows",
                    Description = "A fast-paced techno-thriller about a hacker who uncovers a government conspiracy.",
                    Price = 11.50M
                }, new Book
                {
                    Id = 12,
                    AuthorId = 7,
                    Title = "The Garden of Echoes",
                    Description = "A lyrical tale of loss, memory, and finding one's roots in an ancestral home.",
                    Price = 13.25M
                }, new Book
                {
                    Id = 13,
                    AuthorId = 8,
                    Title = "City of Broken Glass",
                    Description = "In a noir-inspired metropolis, a detective must unravel a string of mysterious disappearances.",
                    Price = 12.00M
                }, new Book
                {
                    Id = 14,
                    AuthorId = 9,
                    Title = "Monsoon Whispers",
                    Description = "A poignant coming-of-age story set in southern India during the monsoon season.",
                    Price = 10.75M
                }, new Book
                {
                    Id = 15,
                    AuthorId = 10,
                    Title = "The Tides of Isla Roja",
                    Description = "An adventure novel centered on a treasure-hunting expedition gone awry.",
                    Price = 15.00M
                }, new Book
                {
                    Id = 16,
                    AuthorId = 11,
                    Title = "Silk and Steel",
                    Description = "A historical fantasy blending martial arts, court intrigue, and ancient magic.",
                    Price = 14.50M
                }, new Book
                {
                    Id = 17,
                    AuthorId = 12,
                    Title = "The Algorithm of Fate",
                    Description = "A speculative fiction novel where destiny is determined by an AI system.",
                    Price = 13.99M
                }, new Book
                {
                    Id = 18,
                    AuthorId = 13,
                    Title = "Veil of Jasmine",
                    Description = "A poetic novel exploring family honor, tradition, and rebellion in the Middle East.",
                    Price = 12.95M
                }, new Book
                {
                    Id = 19,
                    AuthorId = 14,
                    Title = "Frozen Fjords",
                    Description = "A Nordic mystery set in a remote village where the past resurfaces with deadly intent.",
                    Price = 11.80M
                });
        }
    }
}
