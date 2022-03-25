// --- About this program ---
// This program is a sample project which tries to use as many different features of ASP.NET
// as possible and explaining them in the source code through comments.
// It is NOT meant to be written in the most efficient or in the cleanest way, this is
// Program only exists to educate/practise


using System.Globalization;
using backend_learning.Extensions;
using backend_learning.Middleware;


// The builder of the web application
var builder = WebApplication.CreateBuilder(args);


// --- Services ---
// While the builder is not built (through builder.Build()), you can add services to it, which
// can be used by every non-static method by dependency injection (Constructor injection or Method Injection with the
// attibute [FromServices] infront of parameters). This process will be automatic after adding the services.
// There are different scopes which you can choose for your services:
// - Singleton  (Same instance of the class through out the whole application's lifetime)
// - Scoped  (The same instance of the class for every HTTP request)
// - Transistant  (A new instance of the class, everytime the class is injected)
// Libraries usually have extension methods for "IServiceCollection", here "builder.Services"
// e.g. "builder.Services.AddControllers();". You need to add by your own ones by yourself, by using
// e.g. "builder.Services.AddScoped<IInterface, Class>" 
builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;    // Respects the dataformat the browser requests
    config.ReturnHttpNotAcceptable = true;   // Returns an error (406) if the result format type is not supported (e.g. text/css), else it'd default to JSON
}).AddXmlDataContractSerializerFormatters();     // Add XML serializer
// You can add custom formatters as well

// This is an extension method to register my own services and not clutter up the space
builder.Services.AddApplicationServices(builder.Configuration);


var app = builder.Build();  // After the WebApplication was built, you can now use all the services


// --- Startup code ---
// After ASP.NET core 6 removed the "StartUp.cs" class, which made it possible for you to add configuration and service
// code at the beginning of each HTTP request, I use extension methods to add functionallity for these.
// "IServiceCollection.AddApplicationServices" is the extension method I mentioned about, to register my own services
// "WebApplication.AdditionalConfiguration" is my extension method for additional configurations
app.AdditionalConfiguration(app.Services);
app.SeedDatabase();


// --- HTTP request pipeline ---
// This is the pipeline through which each HTTP request runs through, their sequence is important,
// because later functionalities/configurations are dependent on earlier ones.
// You can add your own middle ware here, to create one more step, the app must run through at each HTTP request,
// you add a middleware with the method "WebApplication.UseMiddleware<MiddlewareClass>()" (Look into "ExceptionMiddleware for more info).
app.UseMiddleware<ExceptionMiddleware>();  // This is a custom exception handler middleware (Described in its class)
app.UseHttpsRedirection();  // When connecting to the http address, automatically redirect to the https address
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());  // Allows requests from any source
app.UseAuthorization();  // Adds authorization
app.MapControllers();  // Manages setting up all the different endpoints
app.Run();  // This finally runs the HTTP request after all the configuration