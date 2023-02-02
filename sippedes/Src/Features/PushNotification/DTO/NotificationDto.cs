using Newtonsoft.Json;

namespace sippedes.Features.PushNotification.DTO;

public class NotificationDto
{
    public string DeviceId { get; set; }
    public bool IsAndroidDevice { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}

public class GoogleNotificationDto
{
    public class DataPayload
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
    }
    [JsonProperty("priority")]
    public string Priority { get; set; } = "high";
    [JsonProperty("data")]
    public DataPayload Data { get; set; }
    [JsonProperty("notification")]
    public DataPayload Notification { get; set; }
}