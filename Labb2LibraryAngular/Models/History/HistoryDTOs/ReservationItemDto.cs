using FinalProjectLibrary.Models.Books;
using FinalProjectLibrary.Models.Users;

namespace FinalProjectLibrary.Models
{
    public class ReservationItemDto
    {

        public int ID { get; set; }
        public int UserID { get; set; }
        public int BookID { get; set; }
        public Book Book { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime? AvailabilityDate { get; set; }
        public DateTime? BookIsAvaliableEmailSent { get; set; }
    }
}
