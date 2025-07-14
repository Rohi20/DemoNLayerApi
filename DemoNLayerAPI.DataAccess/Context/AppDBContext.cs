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
        }
    }
}
