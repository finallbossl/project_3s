using FSA_3S.Enum;
using FSA_3S.Models;
using FSA_3S.Models.Entities;
using FSA_3S.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;

namespace FSA_3S.Repositories.Repository
{
    public class StaffRepository(AppDbContext context) : IStaffRepository
    {
        private readonly AppDbContext _context = context;
        /// <summary>
        /// API GET User role staff
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserEntity>> GetUsersByRoleAsync(string role)
        {
            return await _context.Users
                .Where(u => u.Role == role)
                .ToListAsync();
        }
        /// <summary>
        /// API PUT Unable Account for Staff
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UnableStaffResponse> ToggleAccountStatusAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.Role != "Staff")
                return new UnableStaffResponse(false);

            // Chuyển đổi trạng thái trực tiếp với enum
            user.Status = user.Status == UserStatusEnum.Active
                ? UserStatusEnum.Inactive
                : UserStatusEnum.Active;

            await _context.SaveChangesAsync();

            return new UnableStaffResponse(true, user.Status);
        }
        /// <summary>
        /// Work
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserEntity?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }
    }
}