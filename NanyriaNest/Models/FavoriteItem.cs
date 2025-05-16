using FinalProjectLibrary.Models.Books;
using FinalProjectLibrary.Models.Users;
using System.Text.Json.Serialization;

namespace FinalProjectLibrary.Models
{
    public class FavoriteItem
    {
        public int FavoriteId { get; set; }
        public string UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public int BookId { get; set; }
        [JsonIgnore]
        public Book Book { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
