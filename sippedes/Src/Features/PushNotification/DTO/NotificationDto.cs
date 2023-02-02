using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace sippedes.Features.PushNotification.DTO;

public class NotificationDto
{
    public string? UserId { get; set; }
    public string? DeviceToken { get; set; }
    [Required] public string Title { get; set; }

    public bool IsAndroiodDevice { get; set; } = true;
    public string? Body { get; set; }
    
}

public class NotificationResponseDto
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
}

public class GoogleNotificationDto
{
    public class DataPayload
    {
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("body")] public string Body { get; set; }
    }

    [JsonProperty("priority")] public string Priority { get; set; } = "high";
    [JsonProperty("data")] public DataPayload Data { get; set; }
    [JsonProperty("notification")] public DataPayload Notification { get; set; }
}