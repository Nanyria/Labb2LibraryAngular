using FinalProjectLibrary.Models.Books;
using FinalProjectLibrary.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinalProjectLibrary.Models
{
    public class RatingItem
    {

            [Key]
            public int RatingId { get; set; }
            public string UserId { get; set; }
            [JsonIgnore]
            public User User { get; set; }
            public int BookId { get; set; }
            [JsonIgnore]
            public Book Book { get; set; }
            public int Rating { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
    }
}
