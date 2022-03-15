using System.Net;
using System.Text.Json;
using backend_learning.DTOs;


namespace backend_learning.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment() ?
                                new ApiErrorDto(context.Response.StatusCode, ex.Message, ex.StackTrace)
                                : new ApiErrorDto(context.Response.StatusCode, ex.Message);

                var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true };
                var jsonResponse = JsonSerializer.Serialize(response, jsonOptions);

                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}