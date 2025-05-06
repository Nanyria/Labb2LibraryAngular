using FinalProjectLibrary.Models.Books;
using FinalProjectLibrary.Models.Books.BookDTOs;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectLibrary.Models.Users.UserDTOs
{
    public class UserDto
    {
		public int UserID { get; set; }
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public string Email { get; set; }
		public string Password { get; set; }

		public List<BookDto> BorrowedBooks { get; set; } = new List<BookDto>();
		public List<BookDto> ReservedBooks { get; set; } = new List<BookDto>();
		public List<StatusHistoryItemDto> UserHistory { get; set; } = new List<StatusHistoryItemDto>();

    }
}

