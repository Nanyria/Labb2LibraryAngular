using FinalProjectLibrary.Models.Users;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace FinalProjectLibrary.Repositories
{
    public interface IUserRepo
    {
        
        Task CreateUserAsync(User user);        
        Task DeleteUser(User user);
        Task UpdateUser(User user);

        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByUserNameAsync(string userName);


        Task SaveUserAsync();
        IQueryable<User> FindByCondition(Expression<Func<User, bool>> expression);
    }
}
