using FinalProjectLibrary.Models.Books;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectLibrary.Models.Books.BookDTOs
{
    public class CreateBookDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Genre { get; set; }
        public int PublicationYear { get; set; }
        public string BookDescription { get; set; }
        public List<StatusHistoryItem> StatusHistory { get; set; } = new();
    }
}
