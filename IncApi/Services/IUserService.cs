using IncApi.DTOS;

namespace IncApi.Services;

public interface IUserService
{
    Task<string> RegisterAsync(RegisterDto model);
    Task<DatosUsuarioDto> GetTokenAsync(LoginDto model);
    Task<string> AddRoleAsync(AddRolDto model);
    Task<DatosUsuarioDto> VerifyAndGenerateToken(TokenRequestDto tokenRequestDto);

    //Task<string> UpdateRol(AddRolDto model, Rol rol);

}