
namespace Dominio.Interfaces;

public interface IRefreshToken : IRepositoryGeneric<RefreshToken>
{
    Task<RefreshToken> FirstOrDefault(string token );
}

