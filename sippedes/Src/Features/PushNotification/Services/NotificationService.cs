using System.Net.Http.Headers;
using CorePush.Google;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using sippedes.Cores.Entities;
using sippedes.Cores.Exceptions;
using sippedes.Cores.Model;
using sippedes.Cores.Repositories;
using sippedes.Features.PushNotification.DTO;

namespace sippedes.Features.PushNotification.Services;

public interface INotificationService
{
    Task<NotificationResponseDto> SendNotification(NotificationDto notificationModel);

    Task<List<NotificationResponseDto>> GetAllNotifications();
}

public class NotificationService : INotificationService
{
    private readonly FcmConfigurationModel _fcmNotificationSetting;
    private readonly IRepository<DeviceTokenNotification> _repository;
    private readonly IPersistence _persistence;

    public NotificationService(IOptions<FcmConfigurationModel> settings,
        IRepository<DeviceTokenNotification> repository, IPersistence persistence)
    {
        _fcmNotificationSetting = settings.Value;
        _repository = repository;
        _persistence = persistence;
    }

    public async Task<NotificationResponseDto> SendNotification(NotificationDto notificationModel)
    {
        NotificationResponseDto response = new NotificationResponseDto();

        if (notificationModel.IsAndroiodDevice)
        {
            /* FCM Sender (Android Device) */
            FcmSettings settings = new FcmSettings()
            {
                SenderId = _fcmNotificationSetting.SenderId,
                ServerKey = _fcmNotificationSetting.ServerKey
            };
            HttpClient httpClient = new HttpClient();

            string authorizationKey = string.Format("key={0}", settings.ServerKey);
            
            string? deviceToken = (await GetLastUserDeviceTokenNotification(userId: Guid.Parse(notificationModel.UserId ?? "")))?.DeviceToken;
            
            if (deviceToken is null)
            {
                deviceToken = notificationModel.DeviceToken;
                await CreateDeviceNotification(notificationModel);
            };

            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
            httpClient.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            GoogleNotificationDto.DataPayload dataPayload = new GoogleNotificationDto.DataPayload();
            dataPayload.Title = notificationModel.Title;
            dataPayload.Body = notificationModel.Body;

            GoogleNotificationDto notification = new GoogleNotificationDto();
            notification.Data = dataPayload;
            notification.Notification = dataPayload;

            var fcm = new FcmSender(settings, httpClient);
            var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);

            if (fcmSendResponse.IsSuccess())
            {
                response.IsSuccess = true;
                response.Message = "Notification sent successfully";

                await CreateDeviceNotification(notificationDto: notificationModel);
                return response;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = fcmSendResponse.Results[0].Error;
                return response;
            }
        }
        else
        {
            // TODO: IOS implementation
        }

        return response;
    }

    private async Task<DeviceTokenNotification> CreateDeviceNotification(NotificationDto notificationDto)
    {
      var res = await _persistence.ExecuteTransactionAsync( async  () =>
           {
               var entity = await _repository.Save(new DeviceTokenNotification
               {
                   UserCredentialId = Guid.Parse(notificationDto.UserId),
                   DeviceToken = notificationDto.DeviceToken,
                   Title = notificationDto.Title,
                   Body = notificationDto.Body
               });

               await _persistence.SaveChangesAsync();

               return entity;
           }
        );

      return res;
    }

    public async Task<List<NotificationResponseDto>> GetAllNotifications()
    {
        List<DeviceTokenNotification>? notifications = (await _repository.FindAll(notification => true)).ToList();

        List<NotificationResponseDto> res = notifications.Select(notification => new NotificationResponseDto
        {
            IsSuccess = notification.Title is not null,
            Message = notification.Title
        }).ToList();

        return res;
    }

    private async Task<DeviceTokenNotification?> GetLastUserDeviceTokenNotification(Guid userId)
    {
        DeviceTokenNotification? deviceTokenNotification =
            (await _repository.FindAll(criteria: notification => notification.UserCredentialId.Equals(userId),
                orderBy: (notification) => notification.CreatedAt, direction: "DESC")).FirstOrDefault();

        if (deviceTokenNotification is null) return null;

        return deviceTokenNotification;
    }
}