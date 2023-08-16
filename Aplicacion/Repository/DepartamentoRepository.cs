using System.Linq.Expressions;
using Dominio;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class DepartamentoRepository : GenericRepository<Departamento>, IDepartamento
{
  public DepartamentoRepository(IncidenciaContext context) : base(context)
  {

  }
}