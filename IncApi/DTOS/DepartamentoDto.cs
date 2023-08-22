namespace IncApi.DTOS;

public class DepartamentoDto
{
    public string NombreDep { get; set; }
    public string PaisId { get; set; }
    public List<CiudadDTO> Ciudades { get; set; }
}