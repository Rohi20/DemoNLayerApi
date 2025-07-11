using DemoNLayerApi.Data.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoNLayerApi.CustomMiddleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
            
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Call the next middleware in the pipeline
                await _next(context);
            }
            catch(UnauthorizedAccessException uae)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                await context.Response.WriteAsJsonAsync(GetProblemDetails("Unauthorized to access", uae, context));

            }
            catch (NotFoundException nfe)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status404NotFound;

                await context.Response.WriteAsJsonAsync(GetProblemDetails("Resource not found", nfe, context));
            }
            catch(CustomException custEx)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;

                await context.Response.WriteAsJsonAsync(GetProblemDetails("Something went wrong", custEx, context));

            }
            catch (Exception ex)
            {
                //Can add separate catch for not found
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsJsonAsync(GetProblemDetails("Something went wrong", ex, context));
            }
        }
   
        public ProblemDetails GetProblemDetails(string message, Exception ex, HttpContext context)
        {
            return new ProblemDetails
            {
                Title = message,
                Detail = ex.Message,
                Status = context.Response.StatusCode,
                Instance = context.Request.Path,
                Extensions = {
                        ["ErrorMessage"] = ex.InnerException == null ? ex.Message: ex.InnerException,
                    }
            };

        }
    }
}
