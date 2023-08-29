public class Usuario
{
    public int UsuarioId { get; set; }
    public string Username { get; set; }
    public string Email  { get; set; }
    public string  Password { get; set; }   
    public ICollection<Rol>  Roles { get; set; } = new HashSet<Rol>();
    public ICollection<UsuarioRol> UsuarioRoles { get; set; }

}