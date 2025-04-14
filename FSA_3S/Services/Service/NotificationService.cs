using FSA_3S.Models;
using FSA_3S.Repositories.Interface;
using FSA_3S.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FSA_3S.Services.Service
{
    public class NotificationService(AppDbContext context) : INotificationService
    {
        private readonly AppDbContext _context = context;

        public async Task<List<NotificationEntity>> GetNotificationsByUserIdAsync(int userId)
        {
            return await _context.Notifications
                .Where(n => n.ReceiverId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<bool> DeleteNotificationAsync(int notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification == null) return false;

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateNotificationStatusAsync(int notificationId, string status)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification == null) return false;

            notification.IsRead = (status == "Read");
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task NotificationPostRealAsync(int senderId, int receiverId, string title, string message)
        {
            var notification = new NotificationEntity
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Title = title,
                Message = message,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task NotificationPutRealAsync(int senderId, int receiverId, string title, string message)
        {
            var notification = new NotificationEntity
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Title = title,
                Message = message,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }
    }
}