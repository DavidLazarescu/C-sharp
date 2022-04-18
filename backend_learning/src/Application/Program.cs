// --- About this program ---
// This program is a sample project which tries to use as many different features of ASP.NET
// as possible and explaining them in the source code through comments.
// It is NOT meant to be written in the most efficient or in the cleanest way, this is
// Program only exists to educate/practise


using backend_learning.Application.Extensions;
using backend_learning.Application.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using backend_learning.Infrastructure;
using backend_learning.Application.Configuration;


// The builder of the web application
var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;    // Respects the dataformat the browser requests
    config.ReturnHttpNotAcceptable = true;   // Returns an error (406) if the result format type is not supported (e.g. text/css), else it'd default to JSON
}).AddNewtonsoftJson()
  .AddXmlDataContractSerializerFormatters();     // Add XML serializer
// You can add custom formatters as well

// This is an extension method to register my own services and not clutter up the space
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();  // After the WebApplication was built, you can now use all the services


// Startup code
using(var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    
    StartupConfiguration.AdditionalConfiguration(app.Services);
    await StartupConfiguration.SeedDatabaseWithJobsAsync(services.GetRequiredService<DataContext>());
}


// HTTP request pipeline
// You can add your own middle ware here, to create one more step, the app must run through at each HTTP request,
// you add a middleware with the method "WebApplication.UseMiddleware<MiddlewareClass>()" (Look into "ExceptionMiddleware for more info).
app.UseMiddleware<ExceptionMiddleware>();  // This is a custom exception handler middleware (Described in its class)
app.UseHttpsRedirection();  // When connecting to the http address, automatically redirect to the https address
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());  // Allows requests from any source
app.UseAuthorization();  // Adds authorization
app.MapControllers();  // Manages setting up all the different endpoints
app.Run();  // This finally runs the HTTP request after all the configuration