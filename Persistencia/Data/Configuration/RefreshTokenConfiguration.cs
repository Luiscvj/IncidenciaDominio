using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            // Configure entity here
               builder.ToTable("refreshToken");

               builder.Property(x => x.UsuarioId)
                       .HasColumnType("int");
               




















        }
    }