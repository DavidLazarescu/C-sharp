using Application.Common.Middleware;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Seeding;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Presentation;

var builder = WebApplication.CreateBuilder(args);


// Services
builder.Services.AddControllers(config => {
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson()
  .AddXmlDataContractSerializerFormatters();

builder.Services.AddApplicationServices(builder.Configuration);


var app = builder.Build();


// Start up actions
using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    loggerFactory.AddFile(Directory.GetCurrentDirectory() + "/Data/Logs/");

    await DataContextSeeding.SeedDatabaseWithUsers(services.GetRequiredService<DataContext>());
}


// Http pipeline
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseAuthorization();
app.MapControllers();
app.Run();