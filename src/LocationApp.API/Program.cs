global using FastEndpoints;
using FastEndpoints.Swagger;
using LocationApp.Application;
using LocationApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDoc();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseAuthorization();
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseFastEndpoints(x => { x.RoutingOptions = options => { options.Prefix = "api"; }; });
app.UseOpenApi();
app.UseSwaggerUi3(x => { x.ConfigureDefaults(); });

app.Run();