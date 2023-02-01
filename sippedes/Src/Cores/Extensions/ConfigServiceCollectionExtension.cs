using sib_api_v3_sdk.Api;
using sippedes.Cores.Middlewares;
using sippedes.Cores.Repositories;
using sippedes.Cores.Security;
using sippedes.Features.Mail.Services;

namespace sippedes.Cores.Extensions;

public static class ConfigServiceCollectionExtension
{
    public static IServiceCollection AddMyDependencyGroup(
        this IServiceCollection services, ConfigurationManager config)
    {
        services.AddSingleton<TransactionalEmailsApi>();

        services.AddTransient<IMailService, MailService>();
        
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        
        services.AddTransient<IJwtUtils, JwtUtils>();
        // services.AddTransient<IAuthService, AuthService>();

        services.AddTransient<IPersistence, DbPersistence>();
        
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