using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using sippedes.Cores.Middlewares;
using sippedes.Cores.Repositories;
using sippedes.Cores.Security;
using sippedes.Features.Admin.Services;
using sippedes.Features.Auth.Services;
using sippedes.Features.CivilDatas.Services;
using System.Text;

namespace sippedes.Cores.Extensions;

public static class ConfigServiceCollectionExtension
{
    public static IServiceCollection AddMyDependencyGroup(
        this IServiceCollection services, ConfigurationManager config)
    {
        //services.AddSingleton<TransactionalEmailsApi>();

        // Service
        //services.AddTransient<IOtpService, OtpService>();
        //services.AddTransient<IMailService, MailService>();
        services.AddTransient<IJwtUtils, JwtUtils>();
        services.AddTransient<IPersistence, DbPersistence>();
        services.AddTransient<IRoleService, RoleService>();
        services.AddTransient<IAdminDataService, AdminDataService>();
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<ICivilDataService, CivilDataService>();
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

        // Repository
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient<IPersistence, DbPersistence>();

        // Middleware
        services.AddSingleton<ResponseHandlingMiddleware>();

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
