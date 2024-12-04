using Application.Commands.CriarCliente;
using Data;
using Data.Clientes;
using Domain.Cliente.Ports;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

#region swagger
// Adiciona os serviços do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "API", Version = "v1" });
});


#endregion

#region habilitar cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
#endregion

var app = builder.Build();

app.UseCors("AllowAll");

// Habilita o Swagger no ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Cadastro de Clientes v1");
        c.RoutePrefix = string.Empty; // Swagger será acessado na URL raiz
    });
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseHttpsRedirection();

app.Run();