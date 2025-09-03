using GB.Domain.Errors;
using Microsoft.AspNetCore.Mvc;

namespace GB.Middlewares;

public class GlobalErrorMiddleware(RequestDelegate next, ILogger<GlobalErrorMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Une erreur non gérée s'est produite: {Message}", exception.Message );
            await HandleExceptionAsync(context, exception);
        }
    }
    
    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
            
        var problemDetails = new ProblemDetails();

        switch (exception)
        {
            case ArgumentNullException:
            case ArgumentException:
            case EntityAlreadyExistsException:
                problemDetails.Status = StatusCodes.Status400BadRequest;
                problemDetails.Title = "Requête invalide";
                problemDetails.Detail = exception.Message;
                break;

            case KeyNotFoundException:
                problemDetails.Status = StatusCodes.Status404NotFound;
                problemDetails.Title = "Ressource non trouvée";
                break;

            default:
                problemDetails.Status = StatusCodes.Status500InternalServerError;
                problemDetails.Title = "Une erreur interne s'est produite";
                    
                // En production, on va éviter d'exposer les détails techniques
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                {
                    problemDetails.Detail = exception.Message;
                    problemDetails.Instance = exception.StackTrace;
                }
                break;
        }

        context.Response.StatusCode = problemDetails.Status.GetValueOrDefault();

        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}