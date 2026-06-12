using CRUDTareasAPI.Models;
using CRUDTareasAPI.Services;
using Microsoft.AspNetCore.Mvc;
using CRUDTareasAPI.DTOs;

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

        var usuariosDTO = usuarios.Select(u => new UsuarioDTO
        {
            Id = u.Id,
            Nombre = u.Nombre,
            Email = u.Email,
        });

        return Ok(usuariosDTO);
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

        var usuarioDTO = new UsuarioDTO{
            Id = id,
            Nombre= usuario.Nombre,
            Email = usuario.Email
        };

        return Ok(usuarioDTO);
    }

    /// <summary>
    /// Crea un nuevo usuario
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Crear(CrearUsuarioDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        };

        var usuario = new Usuario
        {
            Nombre = dto.Nombre,
            Email = dto.Email,
            Password = dto.Password
        };

        await _service.CrearAsync(usuario);

        var usuarioDTO = new UsuarioDTO
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre,
            Email = usuario.Email
        };

        return Ok(usuarioDTO);
    }

    //PUT: api/usuarios/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, ActualizarUsuarioDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        };

        var usuario = new Usuario
        {
            Nombre = dto.Nombre,
            Email = dto.Email,
            Password = dto.Password
        };

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
