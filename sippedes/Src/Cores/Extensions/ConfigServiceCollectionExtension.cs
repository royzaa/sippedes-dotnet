using System.Text;
using livecode_net_advanced.Cores.Middlewares;
using livecode_net_advanced.Cores.Repositories;
using livecode_net_advanced.Cores.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace livecode_net_advanced.Cores.Extensions;

public static class ConfigServiceCollectionExtension
{
    public static IServiceCollection AddMyDependencyGroup(
        this IServiceCollection services, ConfigurationManager config)
    {
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