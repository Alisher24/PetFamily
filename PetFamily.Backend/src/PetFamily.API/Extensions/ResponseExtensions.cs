using Domain.Enums;
using Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Response;

namespace PetFamily.API.Extensions;

public static class ResponseExtensions
{
    public static ActionResult ToResponse(this Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.AlreadyExists => StatusCodes.Status409Conflict,
            
            _ => StatusCodes.Status500InternalServerError
        };

        var envelope = Envelope.Error(error);
        
        return new ObjectResult(envelope)
        {
            StatusCode =  statusCode
        };
    }
}