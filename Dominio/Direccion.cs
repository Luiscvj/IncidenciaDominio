namespace Dominio;

public class Direccion
{
    public int IdDireccion { get; set; }
    public string TipoVia { get; set; }
    public int Numero { get; set; }
    public string Letra { get; set; }
    public string SufijoCardinal { get; set; }
    public int ? NroViaSecundaria { get; set; }
    public string ? SufijoCardinalSec { get; set; }
    public string PersonaId   { get; set; }
    public Persona Persona { get; set; }

}