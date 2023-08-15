using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class CiudadConfiguration : IEntityTypeConfiguration<Ciudad>
{
    public void Configure(EntityTypeBuilder<Ciudad> builder)
    {
        builder.ToTable("ciudad");

        builder.HasKey(x => x.IdCiudad);
        builder.Property(x => x.IdCiudad)
        .HasMaxLength(3);

        builder.Property(x => x.NombreCiudad)
        .HasMaxLength(50)
        .IsRequired();

        builder.HasOne(x => x.Departamento)
        .WithMany(x => x.Ciudades)
        .HasForeignKey(x =>x.DepartamentoId);


    }

    
}