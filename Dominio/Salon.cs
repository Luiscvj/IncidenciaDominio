namespace Dominio;

public class Salon
{
    public int IdSalon { get; set; }
    public string NombreSalon { get; set; }
    public int Capacidad { get; set; }
    public List<Persona> Personas { get; set; }
    public List<TrainerSalon> TrainerSalones { get; set; }
    public List<Matricula> Matriculas { get; set; }
}