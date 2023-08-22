using System.Linq.Expressions;
using Dominio;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Persistencia;
namespace Aplicacion.Repository;

public class PaisRepository : GenericRepository<Pais>,IPais
{
    private readonly IncidenciaContext _context;
    public PaisRepository(IncidenciaContext context) : base(context)
    {
        _context = context;
    } 

    public override Task<Pais> GetById(string id)
    {
        from pais in _context.Paises
        where pais.PaisId == id &&
        select pais
        
    }
}