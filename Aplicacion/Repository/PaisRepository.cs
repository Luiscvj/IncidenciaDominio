using System.Linq.Expressions;
using Dominio;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class PaisRepository : GenericRepository<Pais>,IPais
{
    
    public PaisRepository(IncidenciaContext context) : base(context)
    {
        
    } 
}