using Microsoft.EntityFrameworkCore;

using vstat_app.Notification.Bll.DbConfiguration;
using vstat_app.Notification.Contracts.Commands.NotificationCommands;
using vstat_app.Notification.Contracts.DTO;
using vstat_app.Notification.Contracts.Interfaces;
using vstat_app.Notification.Contracts.Models;

namespace vstat_app.Notification.Bll;

public class NotificationService : INotificationService
{
    private readonly NotificationDbContext _dbContext;

    public NotificationService(NotificationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<NotificationDto> CreateNotification(string notificationID, NotificationCreateCommand notification)
    {
        Notifications newNotification = new(notificationID,
            notification.UserID,
            notification.CreatedAt,
            notification.Title,
            notification.Body,
            notification.WasRead,
            notification.EventType);

        await _dbContext.Notifications.AddAsync(newNotification);
        await _dbContext.SaveChangesAsync();

        return ModelToDTO(newNotification);
    }

    public async Task<NotificationDto> GetNotificationById(string notificationID)
    {
        var existingNotification = await _dbContext.Notifications.AsNoTracking().FirstOrDefaultAsync(t => t.NotificationID == notificationID)
            ?? throw new ArgumentException("Уведомление не найдено.");

        return ModelToDTO(existingNotification);
    }

    public async Task<ICollection<NotificationDto>> GetAllNotifications() => await _dbContext.Notifications.AsNoTracking()
        .Select(notification => ModelToDTO(notification))
        .ToListAsync();

    public async Task<NotificationDto> UpdateNotification(string notificationID, NotificationUpdateCommand notification)
    {
        var existingNotification = await _dbContext.Notifications.FirstOrDefaultAsync(t => t.NotificationID == notificationID)
            ?? throw new ArgumentException("Уведомление не найдено.");

        existingNotification.UserID = notification.UserID;
        existingNotification.CreatedAt = notification.CreatedAt;
        existingNotification.Title = notification.Title;
        existingNotification.Body = notification.Body;
        existingNotification.WasRead = notification.WasRead;
        existingNotification.EventType = notification.EventType;
        
        await _dbContext.SaveChangesAsync();

        return ModelToDTO(existingNotification);
    }

    public async Task DeleteNotification(string notificationID)
    {
        var notificationToDelete = await _dbContext.Notifications.FirstOrDefaultAsync(t => t.NotificationID == notificationID) 
            ?? throw new ArgumentException("Уведомление не найдено.");
        
        _dbContext.Notifications.Remove(notificationToDelete);
        await _dbContext.SaveChangesAsync();
    }

    private static NotificationDto ModelToDTO(Notifications notification) => new(
        notification.NotificationID,
        notification.UserID,
        notification.CreatedAt,
        notification.Title,
        notification.Body,
        notification.WasRead,
        notification.EventType);

}