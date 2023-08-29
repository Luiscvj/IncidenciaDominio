using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class UsuarioRepository : GenericRepository<Usuario>, IUsuario
{
    public UsuarioRepository(IncidenciaContext context) : base(context)
    {
    }
}