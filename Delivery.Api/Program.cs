using Delivery.Api.Entities;
using Delivery.Domain.ValueObjects;
using Delivery.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Delivery.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContextFactory<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline (middlewares).

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    IDbContextFactory<AppDbContext> factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
    AppDbContext db = factory.CreateDbContext();

    CustomerAggregate customer = CustomerAggregate.Create(
        Guid.Empty,
        PersonalName.Create(new("José"), new("Chaudary")),
        PhoneNumber.Create(new("+5491133345566")),
        Address.Create(new("123 Main St"), new("Springfield"), new("IL"), new("62701"), new("USA")),
        EmailList.Create(Email.Create(new("chaudarysucre@gmail.com"))),
        DateTime.Now
    );

    db.Customers.Add(customer);
    await db.SaveChangesAsync();

    //var s = await db.Customers.FirstAsync();
}

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

await app.RunAsync();