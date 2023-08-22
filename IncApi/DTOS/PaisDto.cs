namespace IncApi.DTOS;

public class PaisDto
{
    public string PaisId { get; set; }
    public string NombrePais { get; set; }
    public List<DepartamentoPaisDto> Departamentos { get; set; }
}