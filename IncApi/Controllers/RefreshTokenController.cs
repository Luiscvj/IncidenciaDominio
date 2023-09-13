using IncApi.DTOS;
using IncApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace IncApi.Controllers;


public class RefreshTokenController : BaseApiController
{

 private readonly IUserService _userService;
 
 public RefreshTokenController(IUserService userService)
 {
    _userService = userService;
 }



    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> RefreshToken([FromBody]TokenRequestDto tokenRequest)
    {
        var result = _userService.VerifyAndGenerateToken(tokenRequest);
        if(result == null)
        {
             return BadRequest();
        }

        return Ok(result);
    }

    }