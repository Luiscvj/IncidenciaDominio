using Dominio.Interfaces;
using IncApi.DTOS;
using IncApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace IncApi.Controllers;


public class RefreshTokenController : BaseApiController
{

 private readonly IUserService _userService;
 private readonly IUnitOfWork  _unitOfWork;
 
 public RefreshTokenController(IUserService userService, IUnitOfWork unitOfWork)
 {
    _userService = userService;
    _unitOfWork = unitOfWork;
 }



    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    /* public async Task<IActionResult> RefreshToken([FromBody]TokenRequestDto tokenRequest)
    {
        var result = _userService.VerifyAndGenerateToken(tokenRequest);
        if(result == null)
        {
             return BadRequest();
        }

        return Ok(result);
    } */


    [HttpPost]
    //[Authorize(Roles="")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

      public async Task<ActionResult<RefreshToken>> Post([FromBody]String token)
      {
          var refresh = await  _unitOfWork.RefreshTokens.FirstOrDefault(token);

          return Ok(refresh);

      }

    

    }