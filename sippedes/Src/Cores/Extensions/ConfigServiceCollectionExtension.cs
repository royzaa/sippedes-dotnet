using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using sippedes.Cores.Middlewares;
using sippedes.Cores.Repositories;
using sippedes.Cores.Security;
using sippedes.Features.Mail.Services;
using sippedes.Features.Otp.Services;
using sippedes.Features.CivilDatas.Services;
using sippedes.Features.Admin.Services;
using sippedes.Features.Auth.Services;
using System.Text;
using CorePush.Apple;
using CorePush.Google;
using Hangfire;
using Hangfire.SqlServer;
using sib_api_v3_sdk.Api;
using sippedes.Cores.Model;
using sippedes.Features.PushNotification.Services;
using sippedes.Features.Letters.Services;
using sippedes.Features.Upload.Services;
using sippedes.Src.Features.LegalizedLetter.Services;
using sippedes.Src.Features.WitnessSignatures.Services;
using sippedes.Features.Users.Services;

namespace sippedes.Cores.Extensions;

public static class ConfigServiceCollectionExtension
{
    public static IServiceCollection AddMyDependencyGroup(
        this IServiceCollection services, ConfigurationManager config)
    {
        services.AddSingleton<TransactionalEmailsApi>();

        // Service
        services.AddTransient<IOtpService, OtpService>();
        services.AddTransient<IMailService, MailService>();
        services.AddTransient<IJwtUtils, JwtUtils>();
        services.AddTransient<IRoleService, RoleService>();
        services.AddTransient<IAdminDataService, AdminDataService>();
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<ICivilDataService, CivilDataService>();
        services.AddTransient<INotificationService, NotificationService>();
        services.AddScoped<ILetterService, LetterService>();
        services.AddScoped<ITrackingStatusService, TrackingStatusService>();
        services.AddScoped<ILetterCategoryService, LetterCategoryService>();
        services.AddTransient<IUploadService, UploadService>();
        services.AddTransient<IUserCredentialService, UserCredentialService>();

        // HttpClient
        services.AddHttpClient<FcmSender>();
        services.AddHttpClient<ApnSender>();

        services.AddTransient<ILegalizedLetterService, LegalizedLetterService>();
        services.AddTransient<IWitnessSignatureService, WitnessSignatureService>();

        // Repository
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient<IPersistence, DbPersistence>();

        // Middleware
        services.AddSingleton<ResponseHandlingMiddleware>();

        // Configure strongly typed settings objects
        var appFcmSettingsSection = config.GetSection("FcmNotification");
        services.Configure<FcmConfigurationModel>(appFcmSettingsSection);
        var appAwsS3SettingSection = config.GetSection("AwsS3");
        services.Configure<AwsS3ConfigurationModel>(appAwsS3SettingSection);

        //HangfireScheduler
        services.AddHangfire(x => x.UseSqlServerStorage(
            $@"Data Source={config.GetConnectionString("HangfireConnection")};Pooling=False"));
        services.AddHangfireServer();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config["JwtSettings:Issuer"],
                ValidAudience = config["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]))
            };
        });

        return services;
    }
}