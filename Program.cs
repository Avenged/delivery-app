using Delivery.Api.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline (middlewares).

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    WeatherForecast[] forecast = [.. Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))];
    return Results.Ok(forecast); // or just: return forecast;
});

app.MapGet("/order", () =>
{
    var order = new Order
    {
        Id = 1,
        CustomerId = 12,
        OrderDate = DateTime.Now,
        Status = "Pending",
        TotalAmount = 19.99m,
        DeliveryAdderss = "Las garzas mz 8 casa 21"
    };
    return Results.Ok(order);
});

await app.RunAsync();