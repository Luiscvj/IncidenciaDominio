
using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;


public class PersistenciConfiguration :IEntityTypeConfiguration<Departamento>
{
    public void Configure(EntityTypeBuilder<Departamento> builder)
    {
        builder.ToTable("departamento");

        builder.HasKey(d => d.IdDep);
        builder.Property(d => d.IdDep)
        .IsRequired()
        .HasMaxLength(3);
        
        
        builder.HasOne(x => x.Pais)
        .WithMany(x => x.Departamentos)
        .HasForeignKey(x => x.PaisId);
    }
}