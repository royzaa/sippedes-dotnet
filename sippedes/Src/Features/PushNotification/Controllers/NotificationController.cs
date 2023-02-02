using Microsoft.AspNetCore.Mvc;
using sippedes.Cores.Controller;
using sippedes.Features.PushNotification.DTO;
using sippedes.Features.PushNotification.Services;

namespace sippedes.Features.PushNotification.Controllers;

[Route("api/notification")]
public class NotificationController : BaseController
{
    private readonly INotificationService _notificationService;
    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }
    
    [HttpPost("push")]
    public async Task<IActionResult> CreateNewNotification([FromBody] NotificationDto notificationDto)
    {
        var res = await _notificationService.SendNotification(notificationDto);

        return Success(res);
    }
    
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllNotification()
    {
        var res = await _notificationService.GetAllNotifications();

        return Success(res);
    }
    
}