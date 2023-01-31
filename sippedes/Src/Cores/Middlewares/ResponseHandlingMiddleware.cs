using System.Net;
using livecode_net_advanced.Cores.Dto;
using sippedes.Cores.Dto;
using sippedes.Cores.Exceptions;

namespace sippedes.Cores.Middlewares;

[Obsolete("Experimental Code")]
public class ResponseHandlingMiddleware : IMiddleware
{
    private Response? _response;
    private readonly ILogger<ResponseHandlingMiddleware> _logger;

    public ResponseHandlingMiddleware(ILogger<ResponseHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception e)
        {
            HandleExceptionAsync(context, e);
            _logger.LogError(e.Message);
        }

        if (!context.Response.HasStarted)
        {
            await context.Response.WriteAsJsonAsync(_response);
        }
    }

    private void HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        _response = new ErrorResponse();

        switch (exception)
        {
            case NotFoundException:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                _response.StatusCode = (int)HttpStatusCode.NotFound;
                _response.Message = exception.Message;
                break;
            case UnauthorizedException:
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                _response.StatusCode = (int)HttpStatusCode.Unauthorized;
                _response.Message = exception.Message;
                break;
            case not null:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                _response.StatusCode = (int)HttpStatusCode.InternalServerError;
                _response.Message = "Internal Server Error";
                break;
        }
    }
}