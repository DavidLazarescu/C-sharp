using System.Net;
using backend_learning.Infrastructure.DTOs.System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace backend_learning.Application.Middleware;

// This is a middleware class, it gets added to the http pipeline, which then gets executed on every HTTP request
// Each Middleware class needs to take a RequestDelegate (this is the method which should be called next in the HTTP pipeline) as
// constructor-argument and needs to define a method called "Invoke" or "InvokeAsync" which takes an HttpContext as argument and calls
// the RequestDelegate (which was given to the constructor, I will call it "_next" here) "await _next(context);".
// This exception middleware has the purpose of being ran at the beginning of every HTTP request to chatch and handle
// every unheandled exception coming up the callstack.
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;  // The next HTTP middleware in the pipeline
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
            await _next(context);  // Try calling it, so it eventually returns with an exception
        }
        catch (Exception ex)  // If an exception occurs, handle it appropriately
        {
            _logger.LogError(ex, ex.Message);  // Log error with the logging system

            // Set the properties of the response to the client
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Include the stacktrace in the response, if the server is run in development mode
            var response = _env.IsDevelopment() ?
                            new ApiErrorDto(context.Response.StatusCode, ex.Message, ex.StackTrace)
                            : new ApiErrorDto(context.Response.StatusCode, ex.Message);


            // Write the response back to the user
            await context.Response.WriteAsync(response.ToString());
        }
    }
}