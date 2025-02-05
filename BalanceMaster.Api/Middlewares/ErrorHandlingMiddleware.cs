using BalanceMaster.Domain.Exceptions;
using BalanceMaster.Identity.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using UnauthorizedAccessException = System.UnauthorizedAccessException;

namespace BalanceMaster.Api.Middlewares;

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
        // Default values for ProblemDetails
        var problemDetails = new ProblemDetails
        {
            Title = "An unexpected error occurred.",
            Status = (int)HttpStatusCode.InternalServerError,
            Detail = exception.Message, // Optionally remove this in production
            Instance = context.TraceIdentifier,
        };

        // Customize ProblemDetails based on exception type
        switch (exception)
        {
            case UnauthorizedAccessException:
                problemDetails.Title = "Unauthorized access.";
                problemDetails.Status = (int)HttpStatusCode.Unauthorized;
                problemDetails.Type = nameof(UnauthorizedAccessException);
                break;

            case Identity.Exceptions.UnauthorizedAccessException:
                problemDetails.Title = exception.Message;
                problemDetails.Status = (int)HttpStatusCode.Unauthorized;
                problemDetails.Type = nameof(UnauthorizedAccessException);
                break;

            case AuthenticationExceptions:
                problemDetails.Title = exception.Message;
                problemDetails.Status = (int)HttpStatusCode.Unauthorized;
                problemDetails.Type = nameof(AuthenticationExceptions);
                break;

            case ChangePasswordException ex:
                problemDetails.Title = exception.Message;
                problemDetails.Status = (int)HttpStatusCode.InternalServerError;
                problemDetails.Type = nameof(ChangePasswordException);
                problemDetails.Extensions = ex.Errors?.ToDictionary(x => x.Code, object? (x) => x.Description) ??
                                            problemDetails.Extensions;
                break;

            case IdentityException ex:
                problemDetails.Title = exception.Message;
                problemDetails.Status = (int)HttpStatusCode.BadRequest;
                problemDetails.Type = nameof(IdentityException);
                problemDetails.Extensions = ex.Errors?.ToDictionary(x => x.Code, object? (x) => x.Description) ??
                                            problemDetails.Extensions;
                break;

            case ArgumentNullException:
            case ArgumentException:
                problemDetails.Title = "Invalid request data.";
                problemDetails.Status = (int)HttpStatusCode.BadRequest;
                problemDetails.Type = nameof(ArgumentException);
                break;

            case KeyNotFoundException:
                problemDetails.Title = "Resource not found.";
                problemDetails.Status = (int)HttpStatusCode.NotFound;
                problemDetails.Type = nameof(KeyNotFoundException);
                break;

            case ObjectNotFoundException:
                problemDetails.Title = "Object not found";
                problemDetails.Status = (int)HttpStatusCode.NotFound;
                problemDetails.Type = nameof(ObjectNotFoundException);
                break;

            case InsufficientFundsException:
                problemDetails.Title = "Insufficient funds";
                problemDetails.Status = (int)HttpStatusCode.Forbidden;
                problemDetails.Type = nameof(InsufficientFundsException);
                break;

            case ValidationException:
                problemDetails.Title = "Validation violation";
                problemDetails.Status = (int)HttpStatusCode.BadRequest;
                problemDetails.Type = nameof(ValidationException);
                break;

            case DomainException:
                problemDetails.Title = "Domain specific logic violation";
                problemDetails.Status = (int)HttpStatusCode.BadRequest;
                problemDetails.Type = nameof(DomainException);
                break;
        }

        // Set response details
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = problemDetails.Status.Value;

        // Serialize the ProblemDetails object to JSON
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var result = JsonSerializer.Serialize(problemDetails, options);
        return context.Response.WriteAsync(result);
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class ErrorHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}