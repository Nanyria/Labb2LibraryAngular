using FinalProjectLibrary.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinalProjectLibrary.Models.Books.BookDTOs
{
    public class BookDto
    {
        public int BookID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public GenreEnums Genre { get; set; }
        public string GenreName => Genre.ToString();
        public string BookDescription { get; set; }
        public  int PublicationYear { get; set; }
        public BookStatusEnum BookStatus { get; set; }
        public string BookStatusName => BookStatus.ToString();


    }
}
