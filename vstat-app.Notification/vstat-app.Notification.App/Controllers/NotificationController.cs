using CSharp.Ulid;

using Microsoft.AspNetCore.Mvc;

using vstat_app.Notification.Contracts.Commands.NotificationCommands;
using vstat_app.Notification.Contracts.Interfaces;
using vstat_app.Notification.Contracts.DTO;

namespace vstat_app.Notification.App.Controllers;

[ApiController]
[Route("api/notifications")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ActionResult))]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNotification([FromBody] NotificationCreateCommand notification)
    {
        string notificationID = Ulid.NewUlid().ToString();

        try
        {
            await _notificationService.CreateNotification(notificationID, notification);

            return CreatedAtAction(nameof(GetNotificationById), new { notificationID }, null);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{notificationID}")]
    public async Task<ActionResult<NotificationDto>> GetNotificationById(string notificationID)
    {
        try
        {
            var notification = await _notificationService.GetNotificationById(notificationID);

            return Ok(notification);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<NotificationDto>>> GetAllNotifications()
    {
        var notifications = await _notificationService.GetAllNotifications();

        return Ok(notifications);
    }

    [HttpPut("{notificationID}")]
    public async Task<IActionResult> UpdateNotification(string notificationID, [FromBody] NotificationUpdateCommand notification)
    {
        try
        {
            await _notificationService.UpdateNotification(notificationID, notification);

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{notificationID}")]
    public async Task<IActionResult> DeleteNotification(string notificationID)
    {
        try
        {
            await _notificationService.DeleteNotification(notificationID);

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }
}