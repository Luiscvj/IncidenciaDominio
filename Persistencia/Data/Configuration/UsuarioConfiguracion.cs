using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;


public class UsuarioConfiguracion : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuario");

        builder.Property(p => p.Username)
        .HasMaxLength(200);

        builder.Property(p => p.Email)
        .HasMaxLength(200);

        builder.HasAlternateKey( u => new  {u.Email, u.Username});

        builder.HasMany(u => u.Roles)
            .WithMany(r => r.Usuarios)
            .UsingEntity<UsuarioRol>
            ();



            
    }
}