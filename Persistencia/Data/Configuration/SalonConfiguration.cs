using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;


public class SalonConfiguration : IEntityTypeConfiguration<Salon>
{
    public void Configure(EntityTypeBuilder<Salon> builder)
    {
        builder.ToTable("salon");
        builder.HasKey( s => s.IdSalon);

        builder.Property(s =>  s.NombreSalon)
        .IsRequired()
        .HasMaxLength(20);

        
    }
}