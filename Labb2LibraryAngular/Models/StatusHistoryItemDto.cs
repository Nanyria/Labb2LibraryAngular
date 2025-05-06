using FinalProjectLibrary.Enums;
using FinalProjectLibrary.Models.Books;
using FinalProjectLibrary.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectLibrary.Models
{
    public class StatusHistoryItemDto
    {
        public int StatusHistoryItemID { get; set; }
        public BookStatusEnum BookStatus { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public int? UserID { get; set; } 
        public string Notes { get; set; }
    }
}
