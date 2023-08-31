namespace Dominio.Interfaces;

public interface IUsuario : IRepositoryGeneric<Usuario>
{

    Task<Usuario> GetByUserAsync(string username);
    
}