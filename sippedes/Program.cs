using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using sib_api_v3_sdk.Client;
using sippedes.Commons.Constants;
using sippedes.Cores.Database;
using sippedes.Cores.Extensions;
using sippedes.Cores.Middlewares;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Configuration.Default.AddApiKey("api-key", builder.Configuration["SendinblueApiKey"]);

        builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "SippedesApi", Version = "v1" });


            option.AddSecurityDefinition(JwtAuthenticationDefaults.AuthenticationScheme,
                new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = JwtAuthenticationDefaults.HeaderName, // Authorization
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtAuthenticationDefaults.AuthenticationScheme
                        }
                    },
                    new List<string>()
                }
            });
        });

        builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
        {
            optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        builder.Services.AddMyDependencyGroup(builder.Configuration);


        var app = builder.Build();


        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sippedes API");
            });
        }


        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();


        app.UseMiddleware<ResponseHandlingMiddleware>();

        app.MapControllers();


        app.Run();
    }
}