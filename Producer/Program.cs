using Producer;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ProducerService>();

var app = builder.Build();

app.MapPost("/", async ([FromServices] ProducerService service, [FromQuery] string message) =>
{
    return await service.SendMessage(message);
});

app.MapPost("/person", async ([FromServices] ProducerService service, [FromBody] PersonModel person) =>
{
    return await service.SendPeson(person);
});

app.UseHttpsRedirection();

app.Run();