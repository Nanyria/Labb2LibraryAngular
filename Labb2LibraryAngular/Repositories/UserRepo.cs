using FinalProjectLibrary.Data;
using FinalProjectLibrary.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinalProjectLibrary.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext _db;
        public UserRepo(AppDbContext db)
        {
            _db = db;
        }
        public async Task CreateUserAsync(User user)
        {
            await _db.Users.AddAsync(user);
        }

        public async Task DeleteUser(User user)
        {
            _db.Users.Remove(user);
        }
        public async Task UpdateUser(User user)
        {
            _db.Users.Update(user);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _db.Users
                .Include(u => u.UserHistory)
                .Include(u => u.ReservedBooks)
                .Include(u => u.CheckedOutBooks)
                .ToListAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _db.Users
                .Include(u => u.UserHistory)
                .Include(u => u.ReservedBooks)
                .Include(u => u.CheckedOutBooks)
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _db.Users
                .Include(u => u.UserHistory)
                .Include(u => u.ReservedBooks)
                .Include(u => u.CheckedOutBooks)
                .FirstOrDefaultAsync(u => u.UserID == id);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _db.Users
                .Include(u => u.UserHistory)
                .Include(u => u.ReservedBooks)
                .Include(u => u.CheckedOutBooks)
                .FirstOrDefaultAsync(u => u.UserName.ToLower() == userName.ToLower());
        }

        public async Task SaveUserAsync()
        {
            await _db.SaveChangesAsync();
        }

        public IQueryable<User> FindByCondition(Expression<Func<User, bool>> expression)
        {
            return _db.Users.Where(expression);
        }
    }
}
