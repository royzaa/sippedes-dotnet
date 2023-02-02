using sippedes.Cores.Middlewares;
using sippedes.Cores.Repositories;
using sippedes.Cores.Security;
using sippedes.Features.Mail.Services;
using sippedes.Features.Otp.Services;
using sippedes.Src.Features.CivilDatas.Services;

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
        // services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<ICivilDataService, CivilDataService>();

        // Repository
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient<IPersistence, DbPersistence>();
        
        // Middleware
        services.AddSingleton<ResponseHandlingMiddleware>();

        // services.AddAuthentication(options =>
        // {
        //     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        // }).AddJwtBearer(options =>
        // {
        //     options.TokenValidationParameters = new TokenValidationParameters
        //     {
        //         ValidateIssuer = true,
        //         ValidateAudience = true,
        //         ValidateLifetime = false,
        //         ValidateIssuerSigningKey = true,
        //         ValidIssuer = config["JwtSettings:Issuer"],
        //         ValidAudience = config["JwtSettings:Audience"],
        //         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]))
        //     };
        // });


        return services;
    }
}