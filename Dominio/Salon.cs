namespace Dominio;

public class Salon
{
    public int IdSalon { get; set; }
    public string nombreSalon { get; set; }
    public int capacidad { get; set; }
    public List<Persona> Personas { get; set; }
    public List<TrainerSalon> TrainerSalones { get; set; }
    public List<Matricula> Matriculas { get; set; }
}