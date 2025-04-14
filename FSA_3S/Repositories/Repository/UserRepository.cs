using FSA_3S.Models.Entities;
using FSA_3S.Repositories.Interface;
using FSA_3S.Models;
using Microsoft.EntityFrameworkCore;

namespace FSA_3S.Repositories.Repository
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        public readonly AppDbContext _context = context; 
        public async Task<UserEntity?> GetByIdAsync(int userId)
        {
           return await _context.Users.FindAsync(userId);
        }

        public async Task UpdateAsync(UserEntity user)
        {
          _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<UserEntity>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }
        /// <summary>
        /// Notification
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetAdminIdAsync()
        {
            return await _context.Users
                .Where(u => u.Role == "Admin")
                .Select(u => u.UserId)
                .FirstOrDefaultAsync();
        }
    }
}
