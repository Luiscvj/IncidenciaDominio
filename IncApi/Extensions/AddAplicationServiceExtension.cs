using Aplicacion.UnitOfWork;
using AspNetCoreRateLimit;
using Dominio.Interfaces;
using iText.Kernel.XMP.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

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
    
}