using ControleVenda.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace ControleVenda.Api
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Handle the exception and generate a response
                await HandleExceptionAsync(context, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = new { error = exception.Message };
            var payload = JsonSerializer.Serialize(response);

            var statusCode = (int)HttpStatusCode.InternalServerError;

            if (exception.GetType() == typeof(RegraNegocioException) ||
                        exception.GetType() == typeof(ArgumentException))
            {
                statusCode = (int)HttpStatusCode.BadRequest;
            }

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(payload);
        }
    }
}
