using FinalProjectLibrary.Helpers.Enums;
using FinalProjectLibrary.Models.History;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace FinalProjectLibrary.Models.Books
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }
        [Required]
        [MaxLength(100)]
        public required string Title { get; set; }

        [Required]
        [MaxLength(75)]
        public required string Author { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public GenreEnums Genre { get; set; }
        public int PublicationYear { get; set; }
        public string? BookDescription { get; set; }
        public List<StatusHistoryItem> StatusHistory { get; set; } = new();
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public required BookStatusEnum BookStatus { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BookTypeEnums BookType { get; set; }
        public List<ReservationItem> Reservations { get; set; } = new();
        public CheckedOutItem? CheckedOutBy { get; set; }


    }
}
