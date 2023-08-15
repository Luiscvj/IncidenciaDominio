namespace Dominio;

public class Direccion
{
    public int IdDireccion { get; set; }
    public string tipoVia { get; set; }
    public int numero { get; set; }
    public string letra { get; set; }
    public string sufijoCardinal { get; set; }
    public int ? nroViaSecundaria { get; set; }
    public string ? sufijoCardinalSec { get; set; }
    public string PersonaId   { get; set; }
    public Persona Persona { get; set; }

}