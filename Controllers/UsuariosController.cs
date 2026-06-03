using CRUDTareasAPI.Models;
using CRUDTareasAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRUDTareasAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly UsuarioService _service;

    public UsuariosController(UsuarioService service)
    {
        _service = service;
    }

    /// <summary>
    /// Obtiene todos los usuarios
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var usuarios = await _service.ObtenerTodosAsync();
        return Ok(usuarios);
    }

    //GET: api/usuarios/1
    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var usuario = await _service.ObtenerPorIdAsync(id);

        if (usuario == null)
        {
            return NotFound();
        }

        return Ok(usuario);
    }

    /// <summary>
    /// Crea un nuevo usuario
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Crear(Usuario usuario)
    {
        await _service.CrearAsync(usuario);

        return Ok(usuario);
    }

    //PUT: api/usuarios/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, Usuario usuario)
    {
        bool actualizado = await _service.ActualizarAsync(id, usuario);

        if (!actualizado)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/usuarios/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        bool eliminado =
            await _service.EliminarAsync(id);

        if (!eliminado)
        {
            return NotFound();
        }

        return NoContent();
    }
}
