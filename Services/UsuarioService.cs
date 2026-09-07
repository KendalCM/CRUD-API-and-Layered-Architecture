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
    private readonly IPasswordHasher _passwordHasher;

    public UsuarioService(UsuarioRepository repository, IMapper mapper, IPasswordHasher passwordHasher)
    {
        _repository = repository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
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

    public async Task<Usuario> CrearAsync(CrearUsuarioDTO dto)
    {
        //Verificar duplicados
        var usuarioExistente = await _repository.ObtenerPorEmailAsync(dto.Email);

        if (usuarioExistente != null)
        {
            throw new EmailDuplicadoException();
        }

        var usuario = _mapper.Map<Usuario>(dto);

        usuario.Password = _passwordHasher.HashPassword(dto.Password);

        await _repository.CrearAsync(usuario);

        return usuario;
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

        usuario.Password = _passwordHasher.HashPassword(dto.Password);

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
