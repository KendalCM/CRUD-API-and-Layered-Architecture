using CRUDTareasAPI.Models;
using CRUDTareasAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRUDTareasAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TareasController : ControllerBase
{
    private readonly TareaService _service;

    public TareasController(TareaService service)
    {
        _service = service;
    }

    //GET: api/tareas
    [HttpGet]
    public async Task<IActionResult> ObtenerTodas()
    {
        var tareas = await _service.ObtenerTodasAsync();
        return Ok(tareas);
    }

    //Get: api/tareas/1
    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var tarea = await _service.ObtenerPorIdAsync(id);

        if(tarea == null)
        {
            return NotFound();
        }

        return Ok(tarea);
    }

    //Post: api/tareas
    [HttpPost]
    public async Task<IActionResult> Crear(Tarea tarea)
    {
        await _service.CrearAsync(tarea);

        return CreatedAtAction(
            nameof(ObtenerPorId),
            new {id = tarea.Id},
            tarea
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, Tarea tarea)
    {
        bool actualizada = await _service.ActualizarAsync(id, tarea);
        if (!actualizada)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/tareas/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        bool eliminada = await _service.EliminarAsync(id);

        if (!eliminada)
        {
            return NotFound();
        }

        return NoContent();
    }

}
