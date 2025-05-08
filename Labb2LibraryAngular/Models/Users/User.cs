using FinalProjectLibrary.Models.Books;
using FinalProjectLibrary.Models.History;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectLibrary.Models.Users
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        [MaxLength(100)]
        public required string UserName { get; set; }
        [Required]
        [MaxLength(100)]
        public required string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public required string LastName { get; set; }
        [Required]
        [MaxLength(100)]
        public required string Email { get; set; }
        [Required]
        [MaxLength(100)]
        public required string Password { get; set; }

        public List<CheckedOutItem> CheckedOutBooks { get; set; } = new List<CheckedOutItem>();
        public List<ReservationItem> ReservedBooks { get; set; } = new List<ReservationItem>();
        public List<StatusHistoryItem> UserHistory { get; set; } = new List<StatusHistoryItem>();

    }
}
