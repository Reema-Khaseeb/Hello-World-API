using HelloWorldAPI;
using Serilog;
using Microsoft.OpenApi.Models;
using HelloWorldAPI.Dtos;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog Logging
CustomLoggerConfigurationExtensions.ConfigureLogging();
builder.Host.UseSerilog();

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HelloWorldApi", Version = "v1" });
});

var app = builder.Build();

// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HelloWorldApi v1"));
}

app.MapGet("/hello", (string? name) =>
{
    try
    {
        var response = new HelloResponse(
            string.IsNullOrWhiteSpace(name) ? "Hello, World!" : $"Hello, {name}"
            );

        Log.Information($"Processed /hello endpoint with name: {name}");
        return Results.Ok(response);
    }
    catch (Exception ex)
    {
        Log.Error(ex, "An error occurred while processing /hello");
        return Results.Json(new ErrorResponse("An error occurred", ex.Message), statusCode: 500);
    }
});

app.MapGet("/info", (HttpContext context) =>
{
    try
    {
        var clientAddress = context.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";
        var hostName = System.Net.Dns.GetHostName();
        var requestTime = DateTime.UtcNow.ToString("o"); // ISO8601 format
        var headers = context.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString());

        var response = new InfoResponse(
            Time: requestTime,
            ClientAddress: clientAddress,
            HostName: hostName,
            Headers: headers
            );

        Log.Information($"Processed /info endpoint for client: {clientAddress}");
        return Results.Ok(response);
    }
    catch (Exception ex)
    {
        Log.Error(ex, "An error occurred while processing /info");
        return Results.Json(new ErrorResponse("An error occurred", ex.Message), statusCode: 500);
    }
});

app.Run();
