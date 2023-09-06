using AutoMapper;
using Dominio;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IncApi.Controllers;


public  class SalonControlle : BaseApiController
{

    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public SalonControlle(IUnitOfWork   unitOfWork, IMapper mapper) 
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
 
 [HttpGet("Todos")]
 [Authorize]
 [ProducesResponseType(StatusCodes.Status200OK)]
 public async Task<IEnumerable<Salon>> GetAll()
 {
    return await _unitOfWork.Salones.GetAll();
 }

}