using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;


public class GeneroConfiguration : IEntityTypeConfiguration<Genero>
{
    public void Configure(EntityTypeBuilder<Genero> builder)
    {
        builder.ToTable("GENERO");
        builder.HasKey(g => g.IdGenero);

        builder.Property(g => g.NombreGenero)
        .IsRequired()
        .HasMaxLength(20);
        
    }
}