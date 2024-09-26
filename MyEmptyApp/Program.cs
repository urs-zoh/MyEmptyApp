using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (_, next) =>
{
    var stopwatch = Stopwatch.StartNew();
    await next.Invoke();
    stopwatch.Stop();
    Console.WriteLine($"Request processing time: {stopwatch.ElapsedMilliseconds} ms");
});

app.MapGet("/", () => "Hello World!");

app.MapGet("/about", () => "This is a simple ASP.NET Core Empty application.");

app.MapPost("/echo", async context =>
{
    using var reader = new StreamReader(context.Request.Body);
    var jsonData = await reader.ReadToEndAsync();
    context.Response.ContentType = "application/json";
    await context.Response.WriteAsync(jsonData);
});

app.Run();