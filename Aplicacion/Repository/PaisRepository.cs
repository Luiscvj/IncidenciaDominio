using System.Linq;
using System.Linq.Expressions;
using Dominio;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Persistencia;
using Microsoft.EntityFrameworkCore; 
namespace Aplicacion.Repository;

public class PaisRepository : GenericRepository<Pais>,IPais
{
    private readonly IncidenciaContext _context;
    public PaisRepository(IncidenciaContext context) : base(context)
    {
        _context = context;
    } 

     public async Task<Pais> GetByID(string Id)
     {
          return  await  _context.Paises
                    .Include(p =>p.Departamentos)
                    .FirstOrDefaultAsync(d => d.PaisId == Id);
     }
}