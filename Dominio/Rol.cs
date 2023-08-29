public class Rol
{
    public int RolId { get; set; }
    public string Nombre { get; set; }
    public ICollection<Usuario> Usuarios { get; set; } = new HashSet<Usuario>();
    public ICollection<UsuarioRol> UsuarioRoles { get; set; }
}