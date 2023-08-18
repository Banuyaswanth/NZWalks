using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace NZWalks.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next) 
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch(Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(JsonSerializer.Serialize(
                    new ProblemDetails
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        Type = "https://httpstatuses.com/500",
                        Title = "Something went wrong. Please try again after some time",
                        Detail = ex.Message,
                        Instance = Guid.NewGuid().ToString()
                    }));
            }
        }
    }
}
