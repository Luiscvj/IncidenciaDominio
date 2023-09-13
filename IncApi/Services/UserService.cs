using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dominio;
using Dominio.Interfaces;
using IncApi.DTOS;
using IncApi.Helpers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistencia;


namespace IncApi.Services;

public class UserService :IUserService
{
    private readonly JWT _jwt;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<Usuario> _passwordHasher;
    private readonly IncidenciaContext _context;
    private readonly TokenValidationParameters  _tokenValidationParameters;
  

    public Rol RolPredeterminado { get; private set; }

    //private readonly IJwtGenerador ;

    public UserService(IUnitOfWork unitOfWork, IOptions<JWT> jwt,IPasswordHasher<Usuario> passwordHasher,IncidenciaContext context, TokenValidationParameters tokenValidationParameters )
    {
        _unitOfWork = unitOfWork;
        _jwt = jwt.Value;
        _passwordHasher = passwordHasher;
        _context   = context ;
        _tokenValidationParameters  = tokenValidationParameters;
         
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
                                      .Find(u => u.Username.ToLower() == registerDto.Username.ToLower()).FirstOrDefault();
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
                     //usuario.Roles.Add(rol);
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

        var   jwtSecurityToken= new JwtSecurityToken();
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
            jwtSecurityToken = CreateJwtToken(usuario);
            var refreshTk =  await CreateRefreshToken(jwtSecurityToken,usuario);
            datosUsuarioDto.RefreshToken = refreshTk.Token;
            datosUsuarioDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            datosUsuarioDto.Username = usuario.Username;
            datosUsuarioDto.Email = usuario.Email;
             datosUsuarioDto.Roles =usuario.Roles.Select(x => x.Nombre).ToList();

       
            //datosUsuarioDto.Token = CreateJwtToken(usuario);//Aca le asignamos el token
            return datosUsuarioDto;

        }
        datosUsuarioDto.EstaAutenticado = false;
        datosUsuarioDto.Mensaje = $"Credenciales incorrectas para el usuario {usuario.Username}.";
        
       
        return datosUsuarioDto;

    }
    
    
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
                                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString()),
                                new Claim("uid", usuario.UsuarioId.ToString())
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

  /*   public async Task<LoginDto> UserLogin(LoginDto model)
    {
        var usuario = await _unitOfWork.Usuarios.GetByUserAsync(model.Username);
        var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, model.Password);

        if (resultado == PasswordVerificationResult.Success)
        {
            return model;
        }
        return null;
    } */

    //Metodo ue me crea un refreshtoken 
    public async Task<RefreshToken> CreateRefreshToken(JwtSecurityToken jwtSecurityToken,Usuario usuario)
    {
            var RefreshToken = new RefreshToken()
                {
                    JwtId =  jwtSecurityToken.Id,
                    Token = RandomStringGeneration(23),//Aqui generamos nuestro refresh token
                    AddedDate = DateTime.UtcNow,
                    ExpiryDate = DateTime.UtcNow.AddMonths(6),
                    IsRevoked = false,
                    IsUsed =false,
                    UsuarioId = usuario.UsuarioId

                };
                _context.RefreshTokens.Add(RefreshToken);//guardamos los datos y sus respectivos tokens en la base de datos
                await _context.SaveChangesAsync();

            return RefreshToken;
    }

    //Metodo para crear de forma aleatoria la cadena del token


    private string RandomStringGeneration(int length)
    {
        var random = new Random();
        var chars = "akljdlkaclcj madceiwicr,cpoacsañcvlmjnvm.poiuytrez,2098e903'00f30m4,cx,.c02'";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());

    }

    public async Task<DatosUsuarioDto> VerifyAndGenerateToken(TokenRequestDto tokenRequestDto)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();//Nos permite verificar el token
            
          /*   var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }; */

        try
        {

            _tokenValidationParameters.ValidateLifetime = false;//para testeo se hace false
            var tokenInVerification = jwtTokenHandler.ValidateToken(tokenRequestDto.Token, _tokenValidationParameters, out var validedToken);
            if(validedToken is JwtSecurityToken jwtSecurityToken)//validaciones de token
            {//validamos la firma
                var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature,StringComparison.InvariantCultureIgnoreCase);

                if(result == false)
                    return null;


                
            }

            //verificamos el tiempo de expiracion

            var utcExpiryDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);

            if(expiryDate < DateTime.Now)
            {
                return new DatosUsuarioDto()
                {
                    Errors = new List<string>()
                    {
                        "Expired Token"
                    }
                };
            }



            //verificamos si el token existe o no en la base de datos

            var storedToken =await  _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequestDto.RefreshToken);

            if(storedToken == null)
            {
                 return new DatosUsuarioDto
                 {
                    Errors = new List<string>()
                    {
                        "Invalid  Token"
                    }
                };
            }
            if(storedToken.IsUsed)
            {
                 return new DatosUsuarioDto
                 {
                    Errors = new List<string>()
                    {
                        "Invalid  Token"
                    }
                };
            }

            if(storedToken.IsRevoked)
            {
                 return new DatosUsuarioDto
                 {
                    Errors = new List<string>()
                    {
                        "Invalid  Token"
                    }
                };
            }


            var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            if(storedToken.JwtId != jti)
            {
                 return new DatosUsuarioDto
                 {
                    Errors = new List<string>()
                    {
                        "Invalid  Token"
                    }
                };
            }
            if(storedToken.ExpiryDate < DateTime.UtcNow)
            {
                 return new DatosUsuarioDto
                 {
                    Errors = new List<string>()
                    {
                        "Expired  Token"
                    }
                };
            }


            //Despues de todas las anteriores validaciones podemos tener loibertad de crear un token nuevo
            storedToken.IsUsed = true;
             _context.RefreshTokens.Update(storedToken);
            await _context.SaveChangesAsync();

            //Creamos nuevamente el token, traemos el usuaro segun el token

            var dbUser = await _context.Usuarios.FirstOrDefaultAsync(x => x.UsuarioId == storedToken.UsuarioId);

            LoginDto dbUserLogin = new LoginDto()
            {
                Username = dbUser.Username,
                Password = dbUser.Password
            };

            return  await GetTokenAsync(dbUserLogin);



        }catch(Exception ex)
        { 
            return new DatosUsuarioDto
                 {
                    Errors = new List<string>()
                    {
                        "Server  Error",
                        ex.Message
                    }
                };
        }


    }

    private DateTime  UnixTimeStampToDateTime(long unixTimeStamp)
    {
        var dateTimeval = new DateTime(1970 ,1 ,1,0,0,0,0 ,DateTimeKind.Utc);
        dateTimeval = dateTimeval.AddSeconds(unixTimeStamp).ToUniversalTime();
        return dateTimeval;
    }
}