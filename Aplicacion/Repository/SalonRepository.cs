using System.Linq.Expressions;
using Dominio;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class SalonRepository :GenericRepository<Salon>, ISalon
{
    public SalonRepository(IncidenciaContext context) :base(context)
    {}
}