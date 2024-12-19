using BalanceMaster.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace BalanceMaster.Api.Middlewares;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Define a default status code and response

        var response = new ErrorResponse
        {
            StatusCode = (int)HttpStatusCode.InternalServerError,
            Message = "An unexpected error occurred. Please try again later.",
            Detail = exception.Message // Optionally remove for production
        };

        // Customize status codes for specific exception types
        switch (exception)
        {
            case UnauthorizedAccessException:
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                response.ErrorCode = nameof(UnauthorizedAccessException);
                response.Message = "Unauthorized access.";
                break;

            case ArgumentNullException:
            case ArgumentException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.ErrorCode = nameof(ArgumentException);
                response.Message = "Invalid request data.";

                break;

            case KeyNotFoundException:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.ErrorCode = nameof(KeyNotFoundException);
                response.Message = "Resource not found.";
                break;

            case ObjectNotFoundException:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.ErrorCode = nameof(ObjectNotFoundException);
                response.Message = exception.Message;
                break;

            case InsufficientFundsException:
                response.StatusCode = (int)HttpStatusCode.Forbidden;
                response.ErrorCode = nameof(InsufficientFundsException);
                response.Message = exception.Message;
                break;

            case ValidationException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.ErrorCode = nameof(ValidationException);
                response.Message = exception.Message;
                break;

            case DomainException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.ErrorCode = nameof(DomainException);
                response.Message = exception.Message;
                break;
        }

        // Set response status code and content type
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = response.StatusCode;

        // Serialize the response to JSON
        var result = JsonSerializer.Serialize(response);
        return context.Response.WriteAsync(result);
    }
}

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string ErrorCode { get; set; }
    public string Message { get; set; }
    public string Detail { get; set; }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class ErrorHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}