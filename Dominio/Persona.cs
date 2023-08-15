namespace Dominio;


public class Persona
{
    public string  Id { get; set; }
    public string Nombre { get; set; }
    public string  Apellido  { get; set; }
    public int GeneroId { get; set; }
    public Genero Genero { get; set; }
    public string CiudadId { get; set; }
    public Ciudad Ciudad { get; set; }
    public int TipoPersonaId   { get; set; }
    public TipoPersona TipoPersona { get; set; }
    public List<Direccion> Direcciones { get; set; }
    public List<Salon> Salones { get; set; }
    public List<TrainerSalon> TrainerSalones { get; set; }
    public List<Matricula> Matriculas { get; set; }

}