using System.Linq.Expressions;

namespace Dominio.Interfaces;

public interface IRepositoryGeneric<T>
{
    Task<T> GetById(string Id);
    Task<IEnumerable<T>> GetAll();
    IEnumerable<T> Find(Expression<Func<T,bool>>expression);
    Task<(int totalRegistros,IEnumerable<T> registros)> GetAllAsync(int pageIndex, int pageSize, string search);
    void Add(T Entity);
    void AddRange(IEnumerable<T> entities);
    void Remove(T Entity );
    void RemoveRange(IEnumerable<T> entities);
    void Update(T Entity);

}