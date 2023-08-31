using Dominio;
using Dominio.Interfaces;
using IncApi.DTOS;
using IncApi.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Seguridad.TokenSeguridad;

namespace IncApi.Services;

public class UserService :IUserService
{
    private readonly JWT _jwt;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<Usuario> _passwordHasher;
    private readonly JwtGenerador _jwtGenerador;

    public Rol RolPredeterminado { get; private set; }

    //private readonly IJwtGenerador _jwtGenerador;

    public UserService(IUnitOfWork unitOfWork, IOptions<JWT> jwt,IPasswordHasher<Usuario> passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _jwt = jwt.Value;
        _passwordHasher = passwordHasher;
    }
    public async Task<string> RegisterAsync(RegisterDto registerDto)
    {
       var usuario = new Usuario
       {
           Email = registerDto.Email,
           Username = registerDto.Username
           
       };

       usuario.Password = _passwordHasher.HashPassword(usuario, registerDto.Password);//Me permite hashear la contraseña dada oir el usuario a la hora de registrarse y pasarselo al objeto usuario que se encargara de tomarla para mas adelante ser guardado en la base de datos como hash
       var usuarioExiste = _unitOfWork.Usuarios
                                      .Find(u => u.Username.ToLower() == registerDto.Username.ToLower());
        if(usuarioExiste == null)
        {
            var rolPredeterminado = _unitOfWork.Roles   
                                               .Find(u => u.Nombre == Autorizacion.rol_predeterminado.ToString())
                                               .First();


            try
            {
                usuario.Roles.Add(rolPredeterminado);
                _unitOfWork.Usuarios.Add(usuario);
                await _unitOfWork.SaveChanges();

             return $"El usuario  {registerDto.Username} ha sido registrado exitosamente";

            }
            catch(Exception ex)
            {
                var message = ex.Message;
                return $"Error: {message}";
            }
        }
         else
        {
            return $"El usuario con {registerDto.Username} ya se encuentra registrado.";
        }
    }

   
    public async Task<string> AddRoleAsync(AddRolDto model)
    {
        var usuario = await _unitOfWork.Usuarios
                    .GetByUserAsync(model.Username);
        if (usuario == null)
        {
            return $"No existe algún usuario registrado con la cuenta {model.Username}.";
        }
        var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, model.Password);//Esta linea de codigo me permite verificar que la contraseña que le paso en model coincida con la contraseña guardada en la base de datos.
        if (resultado == PasswordVerificationResult.Success)
        {
            var rolExiste = _unitOfWork.Roles
                                        .Find(u => u.Nombre.ToLower() == model.Rol.ToLower())//Aqui valido si el rol ingresado existe en la base de datos
                                        .FirstOrDefault();
            if (rolExiste != null)
            {
                var usuarioTieneRol = usuario.Roles
                                            .Any(u => u.RolId == rolExiste.RolId);//Verifica si ese usuario tiene el rol que se quiere asignar

                if (usuarioTieneRol == false)
                {
                    usuario.Roles.Add(rolExiste);//Aca le agregamos  el rol al usuario que ingersamos
                    _unitOfWork.Usuarios.Update(usuario);//Guardamos el usuario
                    await _unitOfWork.SaveChanges();
                }
                return $"Rol {model.Rol} agregado a la cuenta {model.Username} de forma exitosa.";
            }
            return $"Rol {model.Rol} no encontrado.";
        }
        return $"Credenciales incorrectas para el usuario {usuario.Username}.";
    }


    public async Task<DatosUsuarioDto> GetTokenAsync(LoginDto model)
    /*
    *ME RETORNA UNA ESTRUCTURA CON LOS datos sobre si esta autenticado o no
    *
      */
    {
        DatosUsuarioDto datosUsuarioDto = new DatosUsuarioDto();
        var usuario = await _unitOfWork.Usuarios
                    .GetByUserAsync(model.Username);//Busca el usuario de acuerdo al username.
        if (usuario == null)
        {
            datosUsuarioDto.EstaAutenticado = false;
            datosUsuarioDto.Mensaje = $"No existe ningún usuario con el username {model.Username}.";
            return datosUsuarioDto;//Retorna un usuario no autenticado
        }
        
        //Como siguiente paso viene a verificar la contraseña
        var result = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, model.Password);
        if (result == PasswordVerificationResult.Success)
        {
            /* 
            *Datos que se crean al estar autenticado.
             */
            datosUsuarioDto.Mensaje = "Ok";
            datosUsuarioDto.EstaAutenticado = true;
            datosUsuarioDto.Username = usuario.Username;
            datosUsuarioDto.Email = usuario.Username;
            datosUsuarioDto.Token = _jwtGenerador.CrearToken(usuario);//Aca le asignamos el token
            return datosUsuarioDto;

        }
        datosUsuarioDto.EstaAutenticado = false;
        datosUsuarioDto.Mensaje = $"Credenciales incorrectas para el usuario {usuario.Username}.";
        return datosUsuarioDto;

    }/* 
    private JwtSecurityToken CreateJwtToken(Usuario usuario)
    {
        var roles = usuario.Roles;
        var roleClaims = new List<Claim>();
        foreach (var role in roles)
        {
            roleClaims.Add(new Claim("roles", role.Nombre));
        }
        var claims = new[]
        {
                                new Claim(JwtRegisteredClaimNames.Sub, usuario.Username),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                                new Claim("uid", usuario.Id.ToString())
                        }
        .Union(roleClaims);
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        Console.WriteLine("", symmetricSecurityKey);
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
            signingCredentials: signingCredentials);
        return jwtSecurityToken;
    }

    public async Task<LoginDto> UserLogin(LoginDto model)
    {
        var usuario = await _unitOfWork.Usuarios.GetByUserAsync(model.Username);
        var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, model.Password);

        if (resultado == PasswordVerificationResult.Success)
        {
            return model;
        }
        return null;
    }

 */
    public Task<string> AddRolesAsync(AddRolDto model)
    {
        throw new NotImplementedException();
    }
}