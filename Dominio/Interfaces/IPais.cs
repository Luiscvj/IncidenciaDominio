namespace Dominio.Interfaces;



public interface IPais : IRepositoryGeneric<Pais>
{
    Task<Pais> GetByIDpd(string Id);
}