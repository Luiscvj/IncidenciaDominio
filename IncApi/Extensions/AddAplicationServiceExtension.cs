
using Aplicacion.UnitOfWork;
using AspNetCoreRateLimit;
using Dominio.Interfaces;
using IncApi.Services;
using iText.Kernel.XMP.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

using System.Text;
using Microsoft.IdentityModel.Tokens;
using IncApi.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections;
using iText.StyledXmlParser.Jsoup.Parser;

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
       // services.AddSingleton(TokenValidationParameters)

        
     
    
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
        
        //Configuration from AppSettings
        services.Configure<JWT>(configuration.GetSection("JWT"));
       byte[] key =Encoding.ASCII.GetBytes(configuration.GetSection(key:"JWT:Key").Value);


       var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };


        services.AddSingleton<TokenValidationParameters>();
        //Adding Athentication - JWT
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(jwt =>
            {
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = tokenValidationParameters;
            });
    }
    
}