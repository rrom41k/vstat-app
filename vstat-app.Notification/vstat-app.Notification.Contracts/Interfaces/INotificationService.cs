using vstat_app.Notification.Contracts.Commands.NotificationCommands;
using vstat_app.Notification.Contracts.DTO;

namespace vstat_app.Notification.Contracts.Interfaces;

public interface INotificationService
{
    Task<NotificationDto> CreateNotification(string notificationID, NotificationCreateCommand notification);
    Task<NotificationDto> GetNotificationById(string notificationID);
    Task<ICollection<NotificationDto>> GetAllNotifications();
    Task<NotificationDto> UpdateNotification(string notificationID, NotificationUpdateCommand notification);
    Task DeleteNotification(string notificationID);
}