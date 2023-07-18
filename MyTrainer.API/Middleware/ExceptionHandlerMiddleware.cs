using System.Net;
using Microsoft.Extensions.Logging;
using MyTrainer.Application.Exceptions;

namespace MyTrainer.API.Middleware;

public class ExceptionHandlerMiddleware
{
    readonly RequestDelegate _next;
    readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        => (_next, _logger) = (next, logger);

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DatabaseException ex)
        {
            await HandleExceptionAsync(context, ex.Message, HttpStatusCode.InternalServerError, "Working with database error", LogLevel.Error);
        }
        catch (GuidIsNullException ex)
        {
            await HandleExceptionAsync(context, ex.Message, HttpStatusCode.NotFound, "Where is ID?", LogLevel.Warning);
        }

        catch (Exception ex) 
        {
            await HandleExceptionAsync(context, ex.Message, HttpStatusCode.InternalServerError, "Internal Server error", LogLevel.Error);
        }
    }


    async Task HandleExceptionAsync(HttpContext context, string exceptionMessage, HttpStatusCode statusCode, string message, LogLevel logLevel)
    {
        _logger.Log(logLevel, exceptionMessage);

        var response = context.Response;
        response.StatusCode = (int)statusCode;

        await response.WriteAsync(message);
    }


}
