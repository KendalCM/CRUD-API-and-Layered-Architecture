namespace CRUDTareasAPI.Exceptions;

public class EmailDuplicadoException : Exception
{
    public EmailDuplicadoException() : base("El email ya está registrado")
    {
    }
}
