using Microsoft.EntityFrameworkCore;
using FinalProjectLibrary.Enums;
using FinalProjectLibrary.Models.Books;
using FinalProjectLibrary.Models.Users;
using FinalProjectLibrary.Models.History;

namespace FinalProjectLibrary.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<StatusHistoryItem> StatusHistoryItems { get; set; }
        public DbSet<CheckedOutItem> CheckOutItems { get; set; }
        public DbSet<ReservationItem> ReservationItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StatusHistoryItem>()
                .HasOne(sh => sh.book)
                .WithMany(b => b.StatusHistory)
                .HasForeignKey(sh => sh.BookID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StatusHistoryItem>()
                .HasOne(sh => sh.user)
                .WithMany(u => u.UserHistory)
                .HasForeignKey(sh => sh.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .Ignore(u => u.CheckedOutBooks) // Remove unused navigation property
                .Ignore(u => u.ReservedBooks); // Remove unused navigation property

            modelBuilder.Entity<User>()
                .Property(u => u.UserID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(seed: 1001, increment: 1);


            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    BookID = 101,
                    Title = "The Great Gatsby",
                    Author = "F. Scott Fitzgerald",
                    Genre = GenreEnums.Fiction,
                    PublicationYear = 1925,
                    BookDescription = "Lorem Ipsum",
                    BookStatus = BookStatusEnum.Available,
                },
                new Book
                {
                    BookID = 102,
                    Title = "To Kill a Mockingbird",
                    Author = "Harper Lee",
                    Genre = GenreEnums.Fiction,
                    PublicationYear = 1960,
                    BookDescription = "Lorem Ipsum",
                    BookStatus = BookStatusEnum.Available,
                },
                new Book
                {
                    BookID = 103,
                    Title = "1984",
                    Author = "George Orwell",
                    Genre = GenreEnums.Fiction,
                    PublicationYear = 1949,
                    BookDescription = "Lorem Ipsum",
                    BookStatus = BookStatusEnum.Available,
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
                    BookStatus = BookStatusEnum.CheckedOut,  // Example status
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