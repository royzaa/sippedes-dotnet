using sib_api_v3_sdk.Api;
using sippedes.Cores.Middlewares;
using sippedes.Cores.Repositories;
using sippedes.Cores.Security;
using sippedes.Features.Mail.Services;
using sippedes.Features.Otp.Services;

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

        // Repository
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient<IPersistence, DbPersistence>();
        
        // Middleware
        services.AddSingleton<ResponseHandlingMiddleware>();

        return services;
    }
}