namespace CRUDTareasAPI.Exceptions;

public class UsuarioNoEncontradoException : Exception
{
    public UsuarioNoEncontradoException() : base("Usuario no encontrado") 
    { }

    public UsuarioNoEncontradoException(string message) : base(message) { }
}
