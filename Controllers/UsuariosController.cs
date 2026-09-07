using CRUDTareasAPI.Models;
using CRUDTareasAPI.Services;
using Microsoft.AspNetCore.Mvc;
using CRUDTareasAPI.DTOs;
using AutoMapper;

namespace CRUDTareasAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly UsuarioService _service;
    private readonly IMapper _mapper;

    public UsuariosController(UsuarioService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtiene todos los usuarios
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var usuarios = await _service.ObtenerTodosAsync();

        var usuariosDTO = _mapper.Map<List<UsuarioDTO>>(usuarios);

        return Ok(usuariosDTO);
    }

    //GET: api/usuarios/1
    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var usuario = await _service.ObtenerPorIdAsync(id);

        var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);

        return Ok(usuarioDTO);
    }

    /// <summary>
    /// Crea un nuevo usuario
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Crear(CrearUsuarioDTO dto)
    {

        var usuario = _mapper.Map<Usuario>(dto);

        await _service.CrearAsync(usuario);

        var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);
        
        return Ok(usuarioDTO);
    }

    //PUT: api/usuarios/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, ActualizarUsuarioDTO dto)
    {

        await _service.ActualizarAsync(id, dto);

        return NoContent();
    }

    // DELETE: api/usuarios/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _service.EliminarAsync(id);

        return NoContent();
    }
}
