using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

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

app.MapGet("/hello", async (string? name) =>
{
    string greeting = string.IsNullOrEmpty(name) ? "Hello, World!" : $"Hello, {name}";
    var response = new { greeting };

    return Results.Ok(response);
});

app.MapGet("/info", async (HttpContext context) =>
{
    var clientAddress = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
    var hostName = System.Net.Dns.GetHostName();
    var requestTime = DateTime.UtcNow.ToString("o"); // ISO8601 format
    var headers = context.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString());

    var response = new
    {
        time = requestTime,
        client_address = clientAddress,
        host_name = hostName,
        headers
    };

    return Results.Ok(response);
});

app.Run();
