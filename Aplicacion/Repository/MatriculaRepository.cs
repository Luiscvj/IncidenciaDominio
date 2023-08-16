using System.Linq.Expressions;
using Dominio;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class MatriculaRepository : GenericRepository<Matricula>, IMatricula
{
    
    public MatriculaRepository(IncidenciaContext context) : base(context)
    {
        
    } 
}