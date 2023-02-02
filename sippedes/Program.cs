using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using sippedes.Cores.Database;
using sippedes.Cores.Extensions;
using sippedes.Cores.Middlewares;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Configuration.Default.AddApiKey("api-key", builder.Configuration["SendinblueApiKey"]);

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
        {
            optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        builder.Services.AddMyDependencyGroup(builder.Configuration);


        var app = builder.Build();


        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();


        app.UseMiddleware<ResponseHandlingMiddleware>();

        app.MapControllers();


        app.Run();
    }
}
