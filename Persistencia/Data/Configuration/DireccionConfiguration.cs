using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace  Persistencia.Data.Configuration;


public class DireccionConfiguration : IEntityTypeConfiguration<Direccion>
{
    public void Configure(EntityTypeBuilder<Direccion> builder)
    {
        builder.ToTable("direccion");
        builder.HasKey(d =>  d.IdDireccion);

        builder.Property(d => d.TipoVia)
        .IsRequired()
        .HasMaxLength(20);

        builder.Property (d => d.Letra)
        .IsRequired()
        .HasMaxLength(20);
        builder.Property (d => d.SufijoCardinal)
        .IsRequired()
        .HasMaxLength(20);
        builder.Property (d => d.SufijoCardinalSec)
        .IsRequired()
        .HasMaxLength(20);


        builder.HasOne(d => d.Persona)
        .WithMany(p => p.Direcciones)
        .HasForeignKey(d => d.PersonaId);
    }
}