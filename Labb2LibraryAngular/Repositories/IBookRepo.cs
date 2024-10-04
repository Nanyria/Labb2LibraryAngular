﻿using Labb2LibraryAngular.Models;

namespace Labb2LibraryAngular.Repositories
{
    public interface IBookRepo
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);
        Task<Book> GetByTitleAsync(string title);

        Task CreateBookAsync(Book book);
        Task UpdateAsync(Book book);
        Task UpdateStockAsync(int id, Book book);
        Task DeleteAsync(Book book);
        Task SaveAsync();
    }
}
