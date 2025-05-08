using FinalProjectLibrary.Models.Books;
using FinalProjectLibrary.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectLibrary.Models.History
{
    public class CheckedOutItem
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime CheckOutDate { get; set; }
        public  DateTime ReturnDate { get; set; }
        public DateTime? ReminderEmailSent { get; set; }
    }
}
