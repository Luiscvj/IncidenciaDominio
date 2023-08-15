namespace Dominio;

public class Genero
{
    public int IdGenero { get; set; }
    public string NombreGenero { get; set; }
    public List<Persona> Personas { get; set; }
}