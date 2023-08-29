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
        //Se hace registro de informacion atraves de clases de DTO
   


    }

    public Task<DatosUsuarioDto> GetTokenAsync(LoginDto model)
    {
        throw new NotImplementedException();
    }

    public Task<string> RegisterAsync(RegisterDto model)
    {
             var persona = new Usuario
        {
            Email = model.Email,
            Username = model.Username,
        };

        persona.Password = _passwordHasher.HashPassword(persona, model.Password);

        var usuarioExites = _unitOfWork.Usuarios
                                        .Find(u => u.Username.toLower() == model.Username.ToLower())
                                        .FirstOr
    }
}