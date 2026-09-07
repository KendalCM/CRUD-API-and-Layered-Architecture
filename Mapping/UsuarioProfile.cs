using AutoMapper;
using CRUDTareasAPI.DTOs;
using CRUDTareasAPI.Models;

namespace CRUDTareasAPI.Mapping;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<Usuario, UsuarioDTO>();
        CreateMap<CrearUsuarioDTO, Usuario>();
        CreateMap<ActualizarUsuarioDTO, Usuario>();
    }
}
