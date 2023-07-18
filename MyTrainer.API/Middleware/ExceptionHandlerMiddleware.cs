using MyTrainer.Application.Exceptions;
using Npgsql;

namespace MyTrainer.API.Middleware;

public class ExceptionHandlerMiddleware
{
    readonly RequestDelegate _next;
    readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        => (_next, _logger) = (next, logger);

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DatabaseException ex)
        {

        }
        catch (Exception ex) 
        {

        }
    }
}
