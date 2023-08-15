namespace Dominio;

public class TipoPersona
{
    public int IdTipoPersona  { get; set; }
    public string descripcion { get; set; }
    public List<Persona> Personas { get; set; }
}