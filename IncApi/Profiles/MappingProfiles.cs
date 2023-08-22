using AutoMapper;
using Dominio;
using IncApi.DTOS;

namespace IncApi.Profiles;



public class MappingProfiles :Profile
{
    public MappingProfiles()
    {
        CreateMap<Ciudad,CiudadDTO>().ReverseMap();
        CreateMap<Departamento,DepartamentoDto>().ReverseMap();
        CreateMap<Departamento,DepartamentoPaisDto>().ReverseMap();
        CreateMap<Pais,PaisDto>().ReverseMap();
    }
}