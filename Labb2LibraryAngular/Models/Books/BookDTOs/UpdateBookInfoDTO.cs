using FinalProjectLibrary.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinalProjectLibrary.Models.Books.BookDTOs
{
    public class UpdateBookInfoDTO
    {

        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public GenreEnums Genre { get; set; }
        public int PublicationYear { get; set; }
        public string BookDescription { get; set; }

    }
}
