using CRUDTareasAPI.Data;
using CRUDTareasAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDTareasAPI.Repositories;

public class TareaRepository
{
    private readonly AppDbContext _context;

    public TareaRepository(AppDbContext context)
    {
        _context = context;
    }

    //Get All
    public async Task<List<Tarea>> ObtenerTodasdAsync()
    {
        return await _context.Tareas.ToListAsync();
    }

    //Get by id
    public async Task<Tarea?> ObtenerPorIdAsync(int id)
    {
        return await _context.Tareas.FindAsync(id);
    }

    //Create
    public async Task CrearAsync(Tarea tarea)
    {
        await _context.Tareas.AddAsync(tarea);
        await _context.SaveChangesAsync();
    }

    //Update
    public async Task ActualizarAsync(Tarea tarea)
    {
        _context.Tareas.Update(tarea);
        await _context.SaveChangesAsync();
    }

    //Delete
    public async Task EliminarAsync(Tarea tarea)
    {
        _context.Tareas.Remove(tarea);
        await _context.SaveChangesAsync();
    }
}
