using FinalProjectLibrary.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectLibrary.Models.Books.BookDTOs
{
    public class CreateBookDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public GenreEnums Genre { get; set; }
        public string BookDescription { get; set; }
        public int PublicationYear { get; set; }
        
        public List<StatusHistoryItem> StatusHistory { get; set; } = new();
        public BookStatusEnum BookStatus { get; set; } = BookStatusEnum.Available;
    }
}
