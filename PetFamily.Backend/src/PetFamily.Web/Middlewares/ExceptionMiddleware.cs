using PetFamily.Core.Models;
using PetFamily.SharedKernel.Shared;

namespace PetFamily.Web.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            
            var responseError = Error.Failure("server.internal", ex.Message);
            var envelope = Envelope.Error(responseError);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsJsonAsync(envelope);
        }
    }
}