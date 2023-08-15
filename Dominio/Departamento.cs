namespace  Dominio;

public class Departamento
{
    public string IdDep { get; set; }
    public string NombreDep { get; set; }
    public string PaisId { get; set; }
    public Pais Pais { get; set; }
    public List<Ciudad> Ciudades { get; set; }
}