namespace Dominio;

public class Ciudad
{
    public string IdCiudad { get; set; }
    public string NombreCiudad { get; set; } 
    public string DepartamentoId { get; set; }
    public Departamento Departamento { get; set; }
    public List<Persona> Personas { get; set; }
}