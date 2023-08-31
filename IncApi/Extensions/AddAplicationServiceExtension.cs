using Aplicacion.Contratos;
using Aplicacion.UnitOfWork;
using AspNetCoreRateLimit;
using Dominio.Interfaces;
using IncApi.Services;
using iText.Kernel.XMP.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Seguridad.TokenSeguridad;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using IncApi.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace IncApi.Extensions;

public static class AddAplicationServiceseExtension
{
    public static void ConfigureCors(this IServiceCollection services)=>
        services.AddCors(Options=>
            Options.AddPolicy("CorsPolicy",builder=>
            builder.AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod()));

    public static void AddAplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork,UnitOfWork>();
        services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IJwtGenerador,JwtGenerador>();
    }


    public static void ConfigureRateLimiting(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<IRateLimitConfiguration,RateLimitConfiguration>();
        services.AddInMemoryRateLimiting();
        services.Configure<IpRateLimitOptions>(options =>
        {
            options.EnableEndpointRateLimiting = true;
            options.StackBlockedRequests = false;
            options.HttpStatusCode =418;
            options.RealIpHeader = "X-Real-IP";
            options.GeneralRules = new List<RateLimitRule> 
            {
                new RateLimitRule
                {
                    Endpoint = "*",
                    Period = "10s",
                    Limit =10
                }
            };
        });
    }


    public static void ConfigureApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = new QueryStringApiVersionReader("ver");

            options.ApiVersionReader = ApiVersionReader.Combine(
                new QueryStringApiVersionReader("ver"),//Codigo que permite usar Query Strings o headres para la esecificacion de la version
                new HeaderApiVersionReader("X-Version" )
            );
        });
    }

    public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
         services.Configure<JWT>(configuration.GetSection("JWT"));
           services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                };
            });
    }
    
}