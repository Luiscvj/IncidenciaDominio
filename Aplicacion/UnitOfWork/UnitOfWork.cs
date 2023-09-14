using Aplicacion.Repository;
using Dominio;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.UnitOfWork;


public class UnitOfWork : IUnitOfWork,IDisposable
{
    private readonly IncidenciaContext _Context;

    public UnitOfWork(IncidenciaContext Context)
    {
        _Context = Context;
    }

    private CiudadRepository _ciudad;
    private DepartamentoRepository _departamento;
    private DireccionRepository _direccion;
    private GeneroRepository _genero;
    private MatriculaRepository _matricula;
    private PaisRepository _pais;
    private PersonaRepository _persona;
    private SalonRepository _salon;
    private TipoPersonaRepository _tipoPersona;
    private TrainerSalonRepository _trainerSalon;
    private UsuarioRepository  _usuario;
    private RefreshTokenRepository _refreshToken;
    private RolRepository _rol;



    public ICiudad Ciudades
    {
        get
        {
            if(_ciudad == null)
            {
                _ciudad = new CiudadRepository( _Context);
            }
            return _ciudad;
        }
    } 
    public IDepartamento Departamentos 
    {
        get
        {
            if(_departamento == null)
            {
                _departamento = new DepartamentoRepository(_Context);
            }
            return _departamento;
        }
    }

    public IDireccion Direcciones 
    {
        get
        {
            if(_direccion == null)
            {
                _direccion = new DireccionRepository(_Context);
            }
            return _direccion;
        }
    }

    public IGenero Generos 
    {   get
        {

            if(_genero == null)
            {
                _genero = new GeneroRepository(_Context);
            }
            return _genero;
        }
    }

    public IMatricula Matriculas 
    {
        get
        {
            if(_matricula == null)
            {
                _matricula = new MatriculaRepository(_Context);
            }
            return _matricula;
        }
    }

    public IPais Paises 
    {
        get
        {
            if(_pais == null)
            {
                _pais = new PaisRepository(_Context);
            }
            return _pais;
        }
    }

    public IPersona Personas 
    {
        get
        {
            if(_persona == null)
            {
                _persona = new PersonaRepository(_Context);
            }
            return _persona;
        }
    }

    public ISalon Salones 
    {
        get
        {
            if(_salon == null)
            {
                _salon = new SalonRepository(_Context);
            }
            return _salon;
        }
    }

    public ITipoPersona TipoPersonas 
    {   
        get
        {

            if(_tipoPersona == null)
            {
                _tipoPersona = new TipoPersonaRepository(_Context);
            }
            return _tipoPersona;
        }
    }

    public ITrainerSalon TrainerSalones 
    {
        get
        {
            if(_trainerSalon == null)
            {
                _trainerSalon = new TrainerSalonRepository(_Context);
            }
            return _trainerSalon;
        }
    }

    public IUsuario Usuarios
    {
        get
        {
            if (_usuario == null)
            {
                _usuario = new UsuarioRepository(_Context);
            }
            return _usuario;
        }
    }

    public IRol Roles 
    {
        get
        {
            if(_rol == null)
            {
                _rol = new RolRepository(_Context);
            }
            return _rol;
        }
    }

   public IRefreshToken RefreshTokens
   {
       get
       {
           if (_refreshToken == null)
           {
               _refreshToken = new RefreshTokenRepository(_Context);
           }
           return _refreshToken;
       }
   }

    public async Task<int> SaveChanges()
    {
        return  await _Context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _Context.Dispose();
    }
}