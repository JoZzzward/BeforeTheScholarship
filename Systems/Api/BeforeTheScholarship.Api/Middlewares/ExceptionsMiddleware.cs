using BeforeTheScholarship.Common.Exceptions;
using BeforeTheScholarship.Common.Extensions;
using BeforeTheScholarship.Common.Responses;
using System.Text.Json;

namespace BeforeTheScholarship.Api.Middlewares
{
    public class ExceptionsMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            ErrorResponse? errorResponse = null;

            try
            {
                await _next.Invoke(context);
            }
            catch (ProcessException ex)
            {
                errorResponse = ex.ToErrorResponse();
            }
            catch(Exception ex)
            {
                errorResponse = ex.ToErrorResponse();
            }
            finally
            {
                if (errorResponse != null)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
                    await context.Response.StartAsync();
                    await context.Response.CompleteAsync();
                }
            }
        }
    }
}
