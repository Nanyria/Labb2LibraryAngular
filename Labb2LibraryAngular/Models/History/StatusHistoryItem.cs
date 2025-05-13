using FinalProjectLibrary.Enums;
using FinalProjectLibrary.Models.Books;
using FinalProjectLibrary.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectLibrary.Models.History
{
    public class StatusHistoryItem
    {
        [Key]
        public int StatusHistoryItemID { get; set; }
        [ForeignKey("Book")]
        public int BookID { get; set; }
        [Required]
        public Book Book { get; set; }
        [ForeignKey("User")]
        public int? UserID { get; set; }
        public User? User { get; set; }
        [Required]

        public BookStatusEnum BookStatus { get; set; }
        public DateTime? Timestamp { get; set; } = DateTime.UtcNow;

        public string? Notes { get; set; }
    }
}
