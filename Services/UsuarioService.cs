using CRUDTareasAPI.Exceptions;
using CRUDTareasAPI.Middlewares;
using CRUDTareasAPI.Models;
using CRUDTareasAPI.Repositories;

namespace CRUDTareasAPI.Services;

public class UsuarioService
{
    private readonly UsuarioRepository _repository;

    public UsuarioService(UsuarioRepository repository)
    {
        _repository = repository;
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

    public async Task ActualizarAsync(int id, Usuario usuarioActualizado)
    {
        var usuario = await _repository.ObtenerPorIdAsync(id);

        if (usuario == null)
        {
            throw new UsuarioNoEncontradoException();
        }

        usuario.Nombre = usuarioActualizado.Nombre;
        usuario.Email = usuarioActualizado.Email;
        usuario.Password = usuarioActualizado.Password;

        await _repository.ActualiazarAsync(usuario);
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
