using Microsoft.AspNetCore.Diagnostics;

namespace Actividad_Caso_2.Infrastructure;

public sealed class ServiceExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var statusCode = exception switch
        {
            ArgumentException => StatusCodes.Status400BadRequest,
            KeyNotFoundException => StatusCodes.Status404NotFound,
            InvalidOperationException => StatusCodes.Status409Conflict,
            _ => 0
        };

        if (statusCode == 0)
            return false;

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(
            new { mensaje = exception.Message },
            cancellationToken);

        return true;
    }
}
