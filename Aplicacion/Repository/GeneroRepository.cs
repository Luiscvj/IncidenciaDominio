using System.Linq.Expressions;
using Dominio;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;


public class GeneroRepository : GenericRepository<Genero>,IGenero
{
    public GeneroRepository(IncidenciaContext context) : base(context)
    {

    }    
}