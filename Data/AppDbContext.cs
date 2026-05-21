using CRUDTareasAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDTareasAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Tarea> Tareas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

}

