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

    public async Task<Usuario?> ObtenerPorIdAsync(int id)
    {
        return await _repository.ObtenerPorIdAsync(id);
    }

    public async Task CrearAsync(Usuario usuario)
    {
        //Validacion nombre
        if (string.IsNullOrWhiteSpace(usuario.Nombre))
        {
            throw new Exception("El nombre es obligatorio");
        }

        //Validacion email
        if (string.IsNullOrWhiteSpace(usuario.Email))
        {
            throw new Exception("El email es obligatorio");
        }

        //Verificar duplicados
        var usuarioExistente = await _repository.ObtenerPorEmailAsync(usuario.Email);

        if (usuarioExistente != null)
        {
            throw new Exception("El email ya esta registrado");
        }

        await _repository.CrearAsync(usuario);
    }

    public async Task<bool> ActualizarAsync(int id, Usuario usuarioActualizado)
    {
        var usuario = await _repository.ObtenerPorIdAsync(id);

        if (usuario == null)
        {
            return false;
        }

        usuario.Nombre = usuarioActualizado.Nombre;
        usuario.Email = usuarioActualizado.Email;
        usuario.Password = usuarioActualizado.Password;

        await _repository.ActualiazarAsync(usuario);

        return true;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var usuario = await _repository.ObtenerPorIdAsync(id);
        
        if(usuario == null)
        {
            return false;
        }

        await _repository.EliminarAsync(usuario);
        return true;
    }
}
