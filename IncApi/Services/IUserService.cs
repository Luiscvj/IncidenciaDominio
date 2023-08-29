using IncApi.DTOS;

namespace IncApi.Services;

public interface IUserService
{
    Task<string> RegisterAsync(RegisterDto model);
    Task<DatosUsuarioDto> GetTokenAsync(LoginDto model);
    Task<string> AddRolesAsync(AddRolDto model);

}