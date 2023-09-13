using IncApi.Controllers;
using IncApi.DTOS;
using IncApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


public class UsuarioController : BaseApiController
{
    private readonly IUserService _userService;


    public UsuarioController(IUserService userService)
    {
        _userService = userService;
    }



    [HttpPost("register")]

    public async Task<ActionResult> RegisterAsync(RegisterDto model)
    {
        var result = await _userService.RegisterAsync(model);
        return Ok(result);
    }



    [HttpPost("token")]

    public async Task<IActionResult> GetToken(LoginDto model)
    {
        var result = await _userService.GetTokenAsync(model);

        return Ok(result);
    }

    [HttpPost("AddRol")]

    public async Task<IActionResult> AddRol(AddRolDto model)
    {
        var result = await _userService.AddRoleAsync(model);
        return Ok(result);
    }
}