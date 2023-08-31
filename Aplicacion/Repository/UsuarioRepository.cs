using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class UsuarioRepository : GenericRepository<Usuario>, IUsuario
{
    private readonly IncidenciaContext _context;
    public UsuarioRepository(IncidenciaContext context) : base(context)
    {
        _context = context; 
    }

    public async Task<Usuario> GetByUserAsync(string username)
    {
         return  await  _context.Usuarios.Include(u =>u.Roles)
                                         .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
    }
}