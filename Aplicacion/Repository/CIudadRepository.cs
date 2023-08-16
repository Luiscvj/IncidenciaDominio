using System.Linq.Expressions;
using Dominio;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;


public class CiudadRepository : GenericRepository<Ciudad>,ICiudad
{
    public CiudadRepository(IncidenciaContext context) :base(context)
    {

    }
}