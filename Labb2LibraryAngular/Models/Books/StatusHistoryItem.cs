using FinalProjectLibrary.Enums;
using FinalProjectLibrary.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectLibrary.Models.Books
{
    public class StatusHistoryItem
    {
        [Key]
        public int StatusHistoryItemID { get; set; }
        [ForeignKey("Book")]
        public int BookID { get; set; }
        public Book book { get; set; }
        [ForeignKey("User")]
        public int? UserID { get; set; }
        public User user { get; set; }

        public BookStatusEnum BookStatus { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public string Notes { get; set; }
    }
}
