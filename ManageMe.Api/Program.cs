using ManageMe.Api.Filters.Handlers;
using ManageMe.Api.Services;
using ManageMe.Application;
using ManageMe.Core;
using ManageMe.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ManageMe.Api;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddDbContext<ManageMeContext>(
            options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
            )
        );
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IPasswordEncoder, PasswordEncoder>();
        builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        builder.Services.AddScoped<RegisterUserUseCase>();
        builder.Services.AddLogging(configure =>
        {
            if(builder.Environment.IsDevelopment())
            {
                configure.AddConsole();
                return;
            }

            configure.AddFile("manage-me.api.{Date}.log");
        });
        builder.Services.AddSingleton<IApplicationLogger, AppLogger>();
        builder.Services.AddSingleton<ApplicationExceptionHandler>();
        
        WebApplication app = builder.Build();

        app.MapControllers();

        app.Run();
    }
}