namespace vstat_app.Notification.Contracts.Models;

public enum EventTypes
{
    Undefined = 0,
    WorkSpaceWasViewed = 1,
    SendEmailMessage = 2,
    FileWasViewed = 3
}

public class Notifications
{
    public string NotificationID { get; set; }
    public string UserID { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public bool WasRead { get; set; }
    public EventTypes EventType { get; set; }

    public Notifications(string notificationID, string userID, DateTime createdAt, string title, string body, bool wasRead, EventTypes eventType)
    {
        NotificationID = notificationID;
        UserID = userID;
        CreatedAt = createdAt;
        Title = title;
        Body = body;
        WasRead = wasRead;
        EventType = eventType;
    }
}
