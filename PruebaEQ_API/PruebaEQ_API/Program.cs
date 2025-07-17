// Archivo de configuración inicial para la API.
// Se encargan aquí los servicios, middleware y seguridad como JWT, CORS, EF Core y Swagger.

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PruebaEQ_API.Models;
using PruebaEQ_API.Services.Implementaciones;
using PruebaEQ_API.Services.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Conexión a la base de datos
builder.Services.AddDbContext<EQContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

// Inyección de dependencias
builder.Services.AddScoped<ILogProcessService, LogProcessService>();
builder.Services.AddScoped<IDocKeyService, DocKeyService>();

// Límite de tamaño de archivo para uploads (máximo 10 MB)
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10 * 1024 * 1024;
});

// Política CORS abierta (para desarrollo o APIs abiertas sin restricción de origen)
string allowAll = "allowAll";
builder.Services.AddCors(options =>
{
    options.AddPolicy(allowAll, policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// Configuración de autenticación por JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var config = builder.Configuration;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]))
    };
});

var app = builder.Build();

// Desarrollo: activa Swagger para documentación de endpoints
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
}

app.UseCors(allowAll);

// Ejecuta migraciones automáticas (útil para desarrollo)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<EQContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
