using Microsoft.EntityFrameworkCore;
using FinalProjectLibrary.Enums;
using FinalProjectLibrary.Models.Books;

namespace FinalProjectLibrary.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    BookID = 101,
                    Title = "The Great Gatsby",
                    Author = "F. Scott Fitzgerald",
                    Genre = "Fiction",
                    PublicationYear = 1925,
                    BookDescription = "Lorem Ipsum"
                },
                new Book
                {
                    BookID = 102,
                    Title = "To Kill a Mockingbird",
                    Author = "Harper Lee",
                    Genre = "Fiction",
                    PublicationYear = 1960,
                    BookDescription = "Lorem Ipsum"
                },
                new Book
                {
                    BookID = 103,
                    Title = "1984",
                    Author = "George Orwell",
                    Genre = "Fiction",
                    PublicationYear = 1949,
                    BookDescription = "Lorem Ipsum"
                }
            );

            modelBuilder.Entity<StatusHistoryItem>().HasData(
                new StatusHistoryItem
                {
                    StatusHistoryItemID = 1,
                    BookID = 101,
                    BookStatus = BookStatusEnum.Available,  // Use an example status from BookStatusEnum
                    Timestamp = DateTime.UtcNow.AddDays(-1),
                    Notes = "Initial status"
                },
                new StatusHistoryItem
                {
                    StatusHistoryItemID = 2,
                    BookID = 102,
                    BookStatus = BookStatusEnum.Borrowed,  // Example status
                    Timestamp = DateTime.UtcNow.AddDays(-2),
                    Notes = "Initial status"
                },
                new StatusHistoryItem
                {
                    StatusHistoryItemID = 3,
                    BookID = 103,
                    BookStatus = BookStatusEnum.Reserved,  // Example status
                    Timestamp = DateTime.UtcNow.AddDays(-3),
                    Notes = "Initial status"
                }
            );
        }
    }
}