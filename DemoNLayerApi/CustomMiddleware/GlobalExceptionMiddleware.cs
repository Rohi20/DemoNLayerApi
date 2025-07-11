using DemoNLayerApi.Business.Exceptions;
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
            catch(NotFoundException nfe)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status404NotFound;

                await context.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Title = "Something went wrong",
                    Detail = nfe.Message,
                    Status = StatusCodes.Status404NotFound,
                    Instance = context.Request.Path,
                    Extensions = {
                        ["ErrorMessage"] = nfe.InnerException,
                    }
                });
            }
            catch (Exception ex)
            {
                //Can add separate catch for not found
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Title = "Something went wrong",
                    Detail = ex.Message,
                    Status = StatusCodes.Status500InternalServerError,
                    Instance = context.Request.Path,
                    Extensions = {
                        ["ErrorMessage"] = ex.InnerException,
                    }
                });
            }
        }
    }
}
