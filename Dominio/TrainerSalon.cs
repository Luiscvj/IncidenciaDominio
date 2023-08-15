namespace Dominio;

public class TrainerSalon
{
    public string TrainerId { get; set; }
    public Persona Persona { get; set; }
    public int SalonId { get; set; }
    public Salon Salon { get; set; }
}