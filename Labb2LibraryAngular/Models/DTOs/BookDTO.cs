using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Labb2LibraryAngular.Models.DTOs
{
    public class BookDTO
    {
        public int BookID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public string Genre { get; set; }
        public string BookDescription { get; set; }
        public  int PublicationYear { get; set; }
        public bool IsInStock { get; set; }
    }
}
