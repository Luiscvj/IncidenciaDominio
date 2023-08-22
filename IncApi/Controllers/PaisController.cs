using AutoMapper;
using Dominio;
using Dominio.Interfaces;
using IncApi.DTOS;
using Microsoft.AspNetCore.Mvc;

namespace IncApi.Controllers;


public class PaisController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PaisController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    [HttpPost("AddPais")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> Post(PaisDto paisD)
    {
        Pais pais = _mapper.Map<Pais>(paisD);

        if (pais == null)
        {
            return BadRequest();
        }
        _unitOfWork.Paises.Add(pais);
        int num = await _unitOfWork.SaveChanges();
        if (num == 0)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Post),new {id = pais.PaisId},pais);
    }

    [HttpPost("AddRangePais")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]


    public async Task<ActionResult> PostRange(PaisDto[] paisd)
    {
        Pais[] paises = _mapper.Map<Pais[]>(paisd);
        if (paises.Length == 0)
        {
            return BadRequest();
        }

        _unitOfWork.Paises.AddRange(paises);
        int num = await _unitOfWork.SaveChanges();

        if(num == 0)
        {
           return BadRequest(); 
        }

        foreach(Pais entidad in paises)
        {
            CreatedAtAction(nameof(PostRange),new {id= entidad.PaisId},entidad);
        }

        return Ok();
    }

    [HttpGet("GetPais{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetPais(string id)
    {
        Pais pais = await _unitOfWork.Paises.GetById(id);
        
    }
}