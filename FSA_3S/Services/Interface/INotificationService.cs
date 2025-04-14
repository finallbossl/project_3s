using FSA_3S.Models;

namespace FSA_3S.Services.Interface
{
    public interface INotificationService
    {
        Task<List<NotificationEntity>> GetNotificationsByUserIdAsync(int userId);
        Task<bool> DeleteNotificationAsync(int notificationId);
        Task<bool> UpdateNotificationStatusAsync(int notificationId, string status);
        Task NotificationPostRealAsync(int senderId, int receiverId, string title, string message);
        Task NotificationPutRealAsync(int senderId, int receiverId, string title, string message);
    }
}