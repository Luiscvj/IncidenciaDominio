namespace IncApi.DTOS;


public class DatosUsuarioDto
{
    public string  Mensaje { get; set; }
    public bool EstaAutenticado { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public string  RefreshToken { get; set; }
    public List<string> Roles {get;set;}
    public List<string>? Errors {get;set;} 
}