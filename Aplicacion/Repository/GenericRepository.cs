using System.Linq.Expressions;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class GenericRepository<T> : IRepositoryGeneric<T> where T : class
{

    private readonly IncidenciaContext _context;

    public GenericRepository(IncidenciaContext context)
    {
        _context = context;
    }
    public virtual void Add(T Entity)
    {
       _context.Set<T>().Add(Entity);
    }

    public virtual void AddRange(IEnumerable<T> entities)
    {
        _context.Set<T>().AddRange(entities);
    }

    public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
       return  _context.Set<T>().Where(expression).ToList();
    }

    public virtual async  Task<IEnumerable<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async virtual Task<(int totalRegistros, IEnumerable<T> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var totalRegistros = await _context.Set<T>().CountAsync();
        var registros = await _context.Set<T>()
            .Skip((pageIndex -1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

            return (totalRegistros, registros);
    }

    public virtual async Task<T> GetById(string Id)
    {
        return await _context.Set<T>().FindAsync(Id);
    }

    public virtual void Remove(T Entity)
    {
        _context.Set<T>().Remove(Entity);
    }

    public virtual  void RemoveRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }

    public void Update(T Entity)
    {
        _context.Set<T>().Update(Entity);
    }
}