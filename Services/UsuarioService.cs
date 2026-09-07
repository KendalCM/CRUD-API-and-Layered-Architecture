using AutoMapper;
using CRUDTareasAPI.DTOs;
using CRUDTareasAPI.Exceptions;
using CRUDTareasAPI.Middlewares;
using CRUDTareasAPI.Models;
using CRUDTareasAPI.Repositories;

namespace CRUDTareasAPI.Services;

public class UsuarioService
{
    private readonly UsuarioRepository _repository;
    private readonly IMapper _mapper;

    public UsuarioService(UsuarioRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<Usuario>> ObtenerTodosAsync()
    {
        return await _repository.ObtenerTodosAsync(); 
    }

    public async Task<Usuario> ObtenerPorIdAsync(int id)
    {
        var usuario = await _repository.ObtenerPorIdAsync(id);

        if (usuario == null) 
        {
            throw new UsuarioNoEncontradoException();
        }

        return usuario;
    }

    public async Task CrearAsync(Usuario usuario)
    {
        //Verificar duplicados
        var usuarioExistente = await _repository.ObtenerPorEmailAsync(usuario.Email);

        if (usuarioExistente != null)
        {
            throw new EmailDuplicadoException();
        }

        await _repository.CrearAsync(usuario);
    }

    public async Task ActualizarAsync(int id, ActualizarUsuarioDTO dto)
    {
        var usuario = await _repository.ObtenerPorIdAsync(id);

        if (usuario == null)
        {
            throw new UsuarioNoEncontradoException();
        }

        var usuarioExistente = await _repository.ObtenerPorEmailExceptoIdAsync(dto.Email, id);

        if (usuarioExistente != null)
        {
            throw new EmailDuplicadoException();
        }

        _mapper.Map(dto, usuario);

        await _repository.ActualizarAsync(usuario);
    }

    public async Task EliminarAsync(int id)
    {
        var usuario = await _repository.ObtenerPorIdAsync(id);
        
        if(usuario == null)
        {
            throw new UsuarioNoEncontradoException();
        }

        await _repository.EliminarAsync(usuario);
    }
}
