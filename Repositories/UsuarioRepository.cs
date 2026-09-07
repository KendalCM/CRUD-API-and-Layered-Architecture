using CRUDTareasAPI.Data;
using CRUDTareasAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDTareasAPI.Repositories;

public class UsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    //Get All
    public async Task<List<Usuario>> ObtenerTodosAsync()
    {
        return await _context.Usuarios.ToListAsync();
    }

    //Get by Id
    public async Task<Usuario?> ObtenerPorIdAsync(int id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    //Get by Email
    public async Task<Usuario?> ObtenerPorEmailAsync(string email)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
    }

    //Buscar un mismo email con diferente id
    public async Task<Usuario?> ObtenerPorEmailExceptoIdAsync(string email, int id)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email && u.Id != id);
    }

    //Create
    public async Task CrearAsync(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
    }

    //Update
    public async Task ActualizarAsync(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
    }

    //Delete
    public async Task EliminarAsync(Usuario usuario)
    {
        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
    }




}
