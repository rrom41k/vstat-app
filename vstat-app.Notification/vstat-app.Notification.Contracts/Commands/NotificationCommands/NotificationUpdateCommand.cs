using vstat_app.Notification.Contracts.Models;

namespace vstat_app.Notification.Contracts.Commands.NotificationCommands;

public record NotificationUpdateCommand(
    string UserID,
    DateTime CreatedAt,
    string Title,
    string Body,
    bool WasRead,
    EventTypes EventType);