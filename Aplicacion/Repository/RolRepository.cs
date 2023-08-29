using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class RolRepository : GenericRepository<Rol>, IRol
{
    public RolRepository(IncidenciaContext context) : base(context)
    {
    }
}