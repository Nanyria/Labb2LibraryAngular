using FinalProjectLibrary.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinalProjectLibrary.Models.Books.BookDTOs
{
    public class UpdateBookStatusDTO
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }
        public int PublicationYear { get; set; }
        public string BookDescription { get; set; }
        public BookStatusEnum BookStatus { get; set; }
    }
}
