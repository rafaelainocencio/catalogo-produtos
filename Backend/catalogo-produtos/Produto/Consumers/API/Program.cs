using Application.Commands.Handlers;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.

builder.Services.AddMediatR(typeof(CriarClienteCommandHandler));
#region Ioc
builder.Services.AddScoped<CriarClienteCommandHandler>();
#endregion

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseHttpsRedirection();

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}