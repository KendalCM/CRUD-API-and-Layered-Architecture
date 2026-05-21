using CRUDTareasAPI.Models;
using CRUDTareasAPI.Repositories;

namespace CRUDTareasAPI.Services;

public class TareaService
{
    private readonly TareaRepository _repository;

    public TareaService(TareaRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Tarea>> ObtenerTodasAsync()
    {
        return await _repository.ObtenerTodasdAsync();
    }

    public async Task<Tarea?> ObtenerPorIdAsync(int id)
    {
        return await _repository.ObtenerPorIdAsync(id);
    }

    public async Task CrearAsync(Tarea tarea)
    {
        if (string.IsNullOrWhiteSpace(tarea.Titulo))
        {
            throw new Exception("El titulo es obligatorio.");
        }

        await _repository.CrearAsync(tarea);
    }

    public async Task<bool> ActualizarAsync(int id, Tarea tareaActualizada)
    {
        var tarea = await _repository.ObtenerPorIdAsync(id);
        if (tarea == null)
        {
            return false;
        }

        tarea.Titulo = tareaActualizada.Titulo;
        tarea.Descipcion = tareaActualizada.Descipcion;
        tarea.Completada = tareaActualizada.Completada;

        await _repository.ActualizarAsync(tarea);

        return true;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var tarea = await _repository.ObtenerPorIdAsync(id);

        if (tarea == null)
        {
            return false;
        }

        await _repository.EliminarAsync(tarea);

        return true;

    }
}
