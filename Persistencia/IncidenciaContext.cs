using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Persistencia;


public class IncidenciaContext : DbContext
{
    public IncidenciaContext(DbContextOptions<IncidenciaContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}