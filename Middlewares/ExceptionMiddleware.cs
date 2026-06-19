using CRUDTareasAPI.Exceptions;
using System.Text.Json;

namespace CRUDTareasAPI.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (EmailDuplicadoException ex)
        {
            context.Response.StatusCode = 409;

            context.Response.ContentType = "application/json";

            var respuesta = new
            {
                mensaje = ex.Message
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(respuesta)
            );
        }
        catch (UsuarioNoEncontradoException ex)
        {
            context.Response.StatusCode = 404;

            context.Response.ContentType = "application/json";

            var respuesta = new
            {
                mensaje = ex.Message
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(respuesta)
            );
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;

            context.Response.ContentType = "application/json";

            var respuesta = new
            {
                mensaje = "Ocurrio un error interno.",
                detalle = ex.Message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(respuesta));
        }
    }
}
