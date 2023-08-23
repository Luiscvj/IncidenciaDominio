namespace Dominio.Interfaces;



public interface IPais : IRepositoryGeneric<Pais>
{
    Task<Pais> GetByID(string Id);
}