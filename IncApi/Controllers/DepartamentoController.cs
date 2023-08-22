using AutoMapper;
using Dominio;
using Dominio.Interfaces;
using IncApi.DTOS;
using Microsoft.AspNetCore.Mvc;

namespace IncApi.Controllers;


public class DepartamentoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DepartamentoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    [HttpPost("AddDepartamento")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> Post(DepartamentoPaisDto DepartamentoD)
    {
        Departamento departamento = _mapper.Map<Departamento>(DepartamentoD);

        if (departamento == null)
        {
            return BadRequest();
        }
        _unitOfWork.Departamentos.Add(departamento);
        int num = await _unitOfWork.SaveChanges();
        if (num == 0)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Post),new {id = departamento.IdDep},departamento);
    }

    [HttpPost("AddRangeDepartamento")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]


    public async Task<ActionResult> PostRange(DepartamentoPaisDto[] departamentod)
    {
        Departamento[] departamentos = _mapper.Map<Departamento[]>(departamentod);
        if (departamentos.Length == 0)
        {
            return BadRequest();
        }

        _unitOfWork.Departamentos.AddRange(departamentos);
        int num = await _unitOfWork.SaveChanges();

        if(num == 0)
        {
           return BadRequest(); 
        }


        foreach(Departamento entidad in departamentos)
        {
            CreatedAtAction(nameof(PostRange),new {id= entidad.PaisId},entidad);
        }

        return Ok();
    }

}