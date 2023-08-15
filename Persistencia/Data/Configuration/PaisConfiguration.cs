using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class PaisConfiguration : IEntityTypeConfiguration<Pais>
{
    public void Configure(EntityTypeBuilder<Pais> builder)
    {
        builder.ToTable("pais");

        builder.HasKey(p  => p.PaisId);
        builder.Property(p => p.PaisId)
        .IsRequired()
        .HasMaxLength(3);

        builder.Property( p => p.NombrePais)
        .IsRequired()
        .HasMaxLength(50);

        
    }
}