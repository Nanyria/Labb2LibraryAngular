using FinalProjectLibrary.Models.Users;

namespace FinalProjectLibrary.Repositories
{
    public interface IUserRepo
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByUserNameAsync(string userName);
        Task<User> Delete(User user);
        Task<User> Update(User user);
        Task<User> Create(User user);
        Task SaveAsync();
    }
}
