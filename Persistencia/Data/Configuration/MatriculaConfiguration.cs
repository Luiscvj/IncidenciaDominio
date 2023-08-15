using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Dominio;
namespace Persistencia.Data.Configuration;

public class MatriculaConfiguration : IEntityTypeConfiguration<Matricula>
{
    public void Configure(EntityTypeBuilder<Matricula> builder)
    {
            builder.ToTable("matricula");
           
           builder.HasKey(m => m.IdMatricula);

           builder.HasOne(m => m.Persona)
           .WithMany(p => p.Matriculas)
           .HasForeignKey(m => m.PersonaId);

           builder.HasOne( m => m.Salon)
           .WithMany( s => s.Matriculas)
           .HasForeignKey(m =>m.SalonId);
           

    }
}