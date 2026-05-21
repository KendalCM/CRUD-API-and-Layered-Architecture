using System.Data;
using System.ComponentModel.DataAnnotations;

namespace CRUDTareasAPI.Models;

public class Usuario
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }
    public DateTime FechaRegistro { get; set; } = DateTime.Now;

}
