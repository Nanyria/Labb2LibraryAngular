using FinalProjectLibrary.Models.Books;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectLibrary.Models.Users
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        public List<Book> BorrowedBooks { get; set; } = new List<Book>();
        public List<Book> ReservedBooks { get; set; } = new List<Book>();
        public List<UserHistory> UserHistory { get; set; } = new List<UserHistory>();

    }
}
