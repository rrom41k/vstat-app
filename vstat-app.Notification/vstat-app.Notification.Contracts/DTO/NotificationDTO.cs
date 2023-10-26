using vstat_app.Notification.Contracts.Models;

namespace vstat_app.Notification.Contracts.DTO;

public record NotificationDto(
    string NotificationID,
    string UserID,
    DateTime CreatedAt,
    string Title,
    string Body,
    bool WasRead,
    EventTypes EventType);
