using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;


public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
{

    public void Configure(EntityTypeBuilder<Persona> builder)
    {
        builder.ToTable("persona");

        builder.HasKey(p=> p.Id);
        builder.Property(p => p.Id)
        .HasMaxLength(20);

        builder.Property(p => p.Nombre)
        .IsRequired()
        .HasMaxLength(50);

        builder.HasOne(p => p.Genero)
        .WithMany(g => g.Personas)
        .HasForeignKey(p => p.GeneroId);

        builder.HasOne(p => p.Ciudad)
        .WithMany(c => c.Personas)
        .HasForeignKey(p => p.CiudadId);

        builder.HasMany(p => p.TipoPersonas)
        .WithMany(tp => tp.Personas)
        .UsingEntity
        (
            j =>
            {
                j.ToTable("persona_TipoPersonas");
               
            }
        );


        builder.HasMany(p => p.Salones)
        .WithMany(s => s.Personas)
        .UsingEntity<TrainerSalon>();
    }

  
}

