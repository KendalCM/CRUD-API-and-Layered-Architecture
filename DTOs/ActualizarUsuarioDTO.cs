using System.ComponentModel.DataAnnotations;

namespace CRUDTareasAPI.DTOs;

public class ActualizarUsuarioDTO
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El email es obligatorio.")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es obligatorio.")]
    [MinLength(8, ErrorMessage = "Minimo 8 caracteres")]
    public string Password { get; set; } = string.Empty;

}
