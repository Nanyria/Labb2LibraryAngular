using FinalProjectLibrary.Models.Books;

namespace FinalProjectLibrary.Repositories
{
    public interface IBookRepo
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);
        Task<List<Book>> GetByTitleAsync(string title);
        Task<List<Book>> GetByAuthorAsync(string author);
        Task CreateBookAsync(Book book);
        Task UpdateAsync(Book book);
        Task UpdateStockAsync(int id, Book book);
        Task DeleteAsync(Book book);
        Task SaveAsync();
    }
}
