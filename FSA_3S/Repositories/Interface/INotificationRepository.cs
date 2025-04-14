using FSA_3S.Models;

namespace FSA_3S.Repositories.Interface
{
    public interface INotificationRepository
    {
        Task<List<NotificationEntity>> GetNotificationsByUserIdAsync(int userId);
        Task<NotificationEntity> GetNotificationByIdAsync(int notificationId);
        Task DeleteAsync(NotificationEntity notification);
        Task UpdateAsync(NotificationEntity notification);
    }
}