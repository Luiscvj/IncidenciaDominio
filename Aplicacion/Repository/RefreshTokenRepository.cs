using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshToken
{
    private IncidenciaContext _context;
    public RefreshTokenRepository(IncidenciaContext context) : base(context)
    {
        _context = context;
    }

    public  async Task<RefreshToken> FirstOrDefault(string token)
    {
        var refresh =  await _context.Set<RefreshToken>().FirstOrDefaultAsync(x => x.Token == token);

        return refresh ;
    }
}