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

     public async Task<Pais> GetByIDpd(string Id)
     {
          return  await  _context.Paises
                    .Include(p =>p.Departamentos)
                    .FirstOrDefaultAsync(d => d.PaisId == Id);
     }

     public override async Task<(int totalRegistros,IEnumerable<Pais> registros)> GetAllAsync(int pageIndex,int pageSize,string search)
     {
        var query = _context.Paises as IQueryable<Pais>;
        if(!string.IsNullOrEmpty(search))
        {
            query  = query.Where(p => p.NombrePais.ToLower().Contains(search));
        }

        var totalRegistros = await query.CountAsync();
        var registros = await query
                                .Include(u => u.Departamentos)
                                .Skip((pageIndex-1)*pageSize)
                                .Take(pageSize)
                                .ToListAsync();
        return ( totalRegistros, registros);
     }
}