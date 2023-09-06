using AutoMapper;
using Dominio;
using Dominio.Interfaces;
using IncApi.DTOS;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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


    [HttpGet("GetTodos")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]


    public async Task<IEnumerable<Departamento>> GetAlle()
    {
        return await _unitOfWork.Departamentos.GetAll();
    }


    [HttpGet("{id}")]
    [MapToApiVersion("1.0")] 
    //[Authorize]//Esto me da la autorizacion segun mi rol
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<DepartamentoDto>> GetPais(string id)
    {
        if(id == null)
        {
            return BadRequest();
        }

       Departamento p =  await      _unitOfWork.Departamentos.GetById(id);
       

        return _mapper.Map<DepartamentoDto>(p);

    }
}