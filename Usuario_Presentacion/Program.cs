using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Usuario_Aplicacion.Handlers;
using Usuario_Dominio.Repositorios;
using Usuario_Infraestructura.Repositorios;
using Usuario_Infraestructura.Configuracion;
using Usuario_Infraestructura.Mapping;
using System.Security.Claims; 
using System.Text.Json; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1. Configuración de MongoDB
var mongoConfig = new MongoConfig();
builder.Configuration.GetSection("MongoConfig").Bind(mongoConfig);
builder.Services.AddSingleton(mongoConfig);
MongoMappingConfig.ConfigurarMapeos();

// 2. Autenticación (JWT) - CON DIAGNÓSTICO Y ROLES
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Jwt:Authority"];
        options.RequireHttpsMetadata = false;

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" ERROR DE TOKEN: {context.Exception.Message}");
                Console.ResetColor();
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" TOKEN VÁLIDO. Procesando Roles...");

                // --- B. MAPEO DE ROLES DE KEYCLOAK ---
                var claimsIdentity = context.Principal!.Identity as ClaimsIdentity;
                if (claimsIdentity != null)
                {
                    var resourceAccess = context.Principal.FindFirst("realm_access");
                    if (resourceAccess != null)
                    {
                        var content = JsonDocument.Parse(resourceAccess.Value);
                        if (content.RootElement.TryGetProperty("roles", out var rolesElement))
                        {
                            foreach (var role in rolesElement.EnumerateArray())
                            {
                                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.GetString()!));
                                Console.WriteLine($"   -> Rol detectado: {role.GetString()}");
                            }
                        }
                    }
                }
                Console.ResetColor();
                return Task.CompletedTask;
            }
        };

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudiences = new[] { "account", builder.Configuration["Jwt:Audience"] }
        };
    });

// 3. MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(ObtenerHistorialActividadUsuarioHandler).Assembly));

// 4. Repositorios
builder.Services.AddScoped<IUsuarioRepo, UsuarioRepo>();
builder.Services.AddScoped<IHistorialActividadRepo, HistorialActividadRepo>();
builder.Services.AddScoped<IAuditoriaRepo, AuditoriaRepo>();

var app = builder.Build();

app.UseCors("DevCorsPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();