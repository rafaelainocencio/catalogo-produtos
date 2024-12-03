using Application.Commands.Handlers;
using Data;
using Data.Clientes;
using Domain.Cliente.Ports;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddMediatR(typeof(CriarClienteCommandHandler));
#region Ioc
builder.Services.AddScoped<CriarClienteCommandHandler>();

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
#endregion

#region DB writing up
var connectionString = builder.Configuration.GetConnectionString("Main");
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(connectionString));
#endregion

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseHttpsRedirection();

app.Run();