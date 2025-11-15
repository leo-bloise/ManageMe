using ManageMe.Api.Filters.Handlers;
using ManageMe.Api.Options;
using ManageMe.Api.Services;
using ManageMe.Application.Services;
using ManageMe.Application.UseCases;
using ManageMe.Core;
using ManageMe.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ManageMe.Api;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        #region Infrastructure

        builder.Services.AddControllers();
        
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                JwtSettings? settings = builder.Configuration.GetRequiredSection(JwtSettings.Option).Get<JwtSettings>();

                ArgumentNullException.ThrowIfNull(settings);

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = settings.Issuer,
                    ValidAudience = settings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret))
                };
            });
        builder.Services.AddAuthorization();

        builder.Services.AddDbContext<ManageMeContext>(
            options => {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                );
                options.UseLazyLoadingProxies();
            }
        );

        builder.Services.AddSingleton<ApplicationExceptionHandler>();
        builder.Services.AddSingleton<UnableToAuthenticateExceptionHandler>();
        builder.Services.AddSingleton<GeneralExceptionHandler>();
        builder.Services.AddScoped<ITokenFactory, TokenFactory>();
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetRequiredSection(JwtSettings.Option));
        builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

        #endregion Infrastructure

        #region Services

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IPasswordEncoder, PasswordEncoder>();
        builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        builder.Services.AddSingleton<IApplicationLogger, AppLogger>();
        builder.Services.AddLogging(configure =>
        {
            if (builder.Environment.IsDevelopment())
            {
                configure.AddConsole();
                return;
            }

            configure.AddFile("manage-me.api.{Date}.log");
        });

        #endregion Services

        #region UseCases

        builder.Services.AddScoped<RegisterUserUseCase>();
        builder.Services.AddScoped<AuthenticateUserUseCase>();
        builder.Services.AddScoped<RegisterTransactionUseCase>();
        builder.Services.AddScoped<GetTransactionsUseCase>();

        #endregion

        WebApplication app = builder.Build();

        app.MapControllers();

        app.Run();
    }
}