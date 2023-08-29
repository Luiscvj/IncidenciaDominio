using Dominio.Interfaces;
using IncApi.DTOS;
using IncApi.Helpers;
using Microsoft.AspNetCore.Identity;

namespace IncApi.Services;

public class UserService :IUserService
{
    private readonly JWT _jwt;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<Usuario> _passwordHasher;
    //private readonly IJwtGenerador _jwtGenerador;

    public UserService(IUnitOfWork unitOfWork, JWT jwt,IPasswordHasher<Usuario> passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _jwt = jwt;
        _passwordHasher = passwordHasher;
    }

    public async Task<string> AddRolesAsync(AddRolDto model)
    {
       
        var usuario = await _unitOfWork.Usuarios
                            .GetByUserNameAsync(model.Username);

        if(usuario == null)
        {
            return $"No existe  algun usuario registrado con al cuenta {model.Username}";
        }

        var resultado = _passwordHasher.VerifyHashedPassword(usuario,usuario.Password, model.Password);
        if(resultado == PasswordVerificationResult.Success)
        {
            var  rolExiste = _unitOfWork.Roles  
                                        .Find(u => u.Nombre.ToLower() == model.Rol.ToLower())
                                        .FirstOrDefault();

            if(rolExiste != null)
            {
                var usuarioTieneRol = usuario.Roles
                                            .Any(u => u.Id == rolExiste.RolId);
            }
        }

    }

    public Task<DatosUsuarioDto> GetTokenAsync(LoginDto model)
    {
        throw new NotImplementedException();
    }

    public async Task<string> RegisterAsync(RegisterDto model)
    {   
         //Se hace registro de informacion atraves de clases de DTO
             var _usuario = new Usuario
        {
            Email = model.Email,
            Username = model.Username,
        };

        _usuario.Password = _passwordHasher.HashPassword(_usuario, model.Password);

        var usuarioExiste = _unitOfWork.Usuarios
                                        .Find(u => u.Username.ToLower() == model.Username.ToLower())
                                        .FirstOrDefault();

        if(usuarioExiste == null)
        {

                var rol_predeterminado = _unitOfWork.Roles
                                                 .Find( u => u.Nombre == Autorizacion.rol_predeterminado.ToString())
                                                 .First();
                                            
            try
            {
                _usuario.Roles.Add(rol_predeterminado);
                _unitOfWork.Usuarios.Add(_usuario);
                await _unitOfWork.SaveChanges();

                return $"El usuario {model.Username} ha sido agregado exitosamente";
            }

            catch(Exception ex)
            {
                var message = ex.Message;
                 return $"Error: {message}";
            }
        }

        else
        {
            return $"El usuario  {model.Username}  ya se encuentra registrado";
        }

    }
}