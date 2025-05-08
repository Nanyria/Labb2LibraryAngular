using FinalProjectLibrary.Models.Books;
using FinalProjectLibrary.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectLibrary.Models.History
{
    public class ReservationItem
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int BookID { get; set; }
        [Required]
        public Book Book { get; set; }
        public DateTime ReservationDate { get; set; }

        public DateTime? AvailabilityDate { get; set; }

        public DateTime? BookIsAvaliableEmailSent { get; set; }
    }
}
