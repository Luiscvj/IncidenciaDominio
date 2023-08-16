using System.Linq.Expressions;
using Dominio;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class PersonaRepository : GenericRepository<Persona>,IPersona
{
   
    public PersonaRepository(IncidenciaContext context) : base(context)
    {
        
    } 
}