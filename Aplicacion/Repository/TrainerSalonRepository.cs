using System.Linq.Expressions;
using Dominio;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class TrainerSalonRepository : GenericRepository<TrainerSalon>, ITrainerSalon
{
    public TrainerSalonRepository(IncidenciaContext context) : base(context)
    {
    }
}