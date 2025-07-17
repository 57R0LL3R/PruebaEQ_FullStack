// Archivo de configuraci�n inicial para la API.
// Se encargan aqu� los servicios, middleware y seguridad como JWT, CORS, EF Core y Swagger.

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PruebaEQ_API.Models;
using PruebaEQ_API.Services.Implementaciones;
using PruebaEQ_API.Services.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Conexi�n a la base de datos
builder.Services.AddDbContext<EQContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

// Inyecci�n de dependencias
builder.Services.AddScoped<ILogProcessService, LogProcessService>();
builder.Services.AddScoped<IDocKeyService, DocKeyService>();

// L�mite de tama�o de archivo para uploads (m�ximo 10 MB)
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10 * 1024 * 1024;
});

// Pol�tica CORS abierta (para desarrollo o APIs abiertas sin restricci�n de origen)
string allowAll = "allowAll";
builder.Services.AddCors(options =>
{
    options.AddPolicy(allowAll, policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// Configuraci�n de autenticaci�n por JWT
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

// Desarrollo: activa Swagger para documentaci�n de endpoints
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
}

app.UseCors(allowAll);

// Ejecuta migraciones autom�ticas (�til para desarrollo)
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
