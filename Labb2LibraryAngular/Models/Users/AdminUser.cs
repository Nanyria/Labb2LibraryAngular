namespace FinalProjectLibrary.Models.Users
{
    public class AdminUser : User
    {
        public string AdminRole { get; set; } // e.g., "SuperAdmin", "Librarian"
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastLogin { get; set; } = DateTime.UtcNow;
        // Additional properties and methods specific to admin users can be added here
        // For example, methods for managing user accounts, books, etc.
    
    }
}
