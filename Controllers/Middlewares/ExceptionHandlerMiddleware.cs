using Domain.Exception;
using Newtonsoft.Json;
using System.Net;

namespace Controllers.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ILogger<ExceptionHandlerMiddleware> logger)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await ConvertException(context, ex, logger);
        }
    }

    private Task ConvertException(HttpContext context, Exception exception, ILogger<ExceptionHandlerMiddleware> logger)
    {
        var httpStatusCode = HttpStatusCode.InternalServerError;

        context.Response.ContentType = "application/json";

        var result = string.Empty;
        switch (exception)
        {
            case ResturantValidationException validationException:
                httpStatusCode = HttpStatusCode.BadRequest;
                result = JsonConvert.SerializeObject(new { error = validationException.ValdationErrors});
                break;
            case BadRequestException badRequestException:
                httpStatusCode = HttpStatusCode.BadRequest;
                result = badRequestException.Message;
                break;
            case NotFoundException notFoundException:
                httpStatusCode = HttpStatusCode.NotFound;
                break;
            case InvalidAccessTokenException invalidAccessTokenException:
                httpStatusCode= HttpStatusCode.Unauthorized;
                result = JsonConvert.SerializeObject(new { error = invalidAccessTokenException.Message });
                break;
            case Exception ex:
                httpStatusCode = HttpStatusCode.InternalServerError;
                break;
        }
        logger.LogError(exception, "error");
        context.Response.StatusCode = (int)httpStatusCode;

        if (result == string.Empty)
        {
            result = JsonConvert.SerializeObject(new { error = exception.Message, innerException = exception.InnerException?.Message });
        }

        return context.Response.WriteAsync(result);
    }


}

public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}
