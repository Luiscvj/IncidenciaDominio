using Aplicacion.UnitOfWork;
using AspNetCoreRateLimit;
using Dominio.Interfaces;
using iText.Kernel.XMP.Options;

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
                    Limit =2
                }
            };
        });
    }
}