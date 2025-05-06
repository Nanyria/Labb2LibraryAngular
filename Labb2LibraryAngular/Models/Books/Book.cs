using FinalProjectLibrary.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectLibrary.Models.Books
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(75)]
        public string Author { get; set; }
        [Required]
        [MaxLength(25)]
        public GenreEnums Genre { get; set; }
        public int PublicationYear { get; set; }
        public string BookDescription { get; set; }
        public List<StatusHistoryItem> StatusHistory { get; set; } = new();
        public BookStatusEnum BookStatus { get; set; }


    }
}
