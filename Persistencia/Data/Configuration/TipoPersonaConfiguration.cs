using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;


public class TipoPersonaConfiguration : IEntityTypeConfiguration<TipoPersona>
{
    public void Configure(EntityTypeBuilder<TipoPersona> builder)
    {
        builder.ToTable("tipo_persona");
        builder.HasKey(tp => tp.IdTipoPersona);

        builder.Property(tp => tp.descripcion)
        .IsRequired()
        .HasMaxLength(50);

        
    }
}