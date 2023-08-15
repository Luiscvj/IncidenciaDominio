namespace Dominio;

public class Pais
{
    public string PaisId { get; set; }
    public string NombrePais { get; set; }
    public List<Departamento> Departamentos { get; set; }
}