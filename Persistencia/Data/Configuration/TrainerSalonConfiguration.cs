using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;


public class TrainerSalonConfiguration : IEntityTypeConfiguration<TrainerSalon>
{
    public void Configure(EntityTypeBuilder<TrainerSalon> builder)
    {
       builder.ToTable("trainer_salon");
    }
}