using Aplicacion.UnitOfWork;
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
}