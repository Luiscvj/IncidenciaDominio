namespace Dominio;

public class Matricula
{
    public int IdMatricula { get; set; }
    public string PersonaId { get; set; }
    public Persona Persona { get; set; }
    public int SalonId { get; set; }
    public Salon Salon { get; set; }
}