using System.Linq.Expressions;
using Dominio;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class DireccionRepository : GenericRepository<Direccion>,IDireccion
{
    public DireccionRepository(IncidenciaContext context) : base(context)
    {

    }
}