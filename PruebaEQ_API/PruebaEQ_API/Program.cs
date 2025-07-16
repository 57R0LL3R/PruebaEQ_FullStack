using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using PruebaEQ_API.Models;
using PruebaEQ_API.Services.Implementaciones;
using PruebaEQ_API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EQContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));
builder.Services.AddScoped<ILogProccesService,LogProccesService>();
builder.Services.AddScoped<IDocKeyService, DocKeyService>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwagger();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<EQContext>();
    db.Database.Migrate();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();
