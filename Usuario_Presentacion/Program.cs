using MediatR;
using Microsoft.Extensions.Options;
using Usuario_Aplicacion.Commands;
using Usuario_Aplicacion.Query;
using Usuario_Dominio.Repositorios;
using Usuario_Infraestructura.Configuracion;
using Usuario_Infraestructura.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CrearUsuarioCommand>());
builder.Services.Configure<MongoConfig>(builder.Configuration.GetSection("MongoConfig"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<MongoConfig>>().Value);
builder.Services.AddSingleton<IUsuarioRepo, UsuarioRepo>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ObtenerUsuarioQuery>());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("DevCorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
