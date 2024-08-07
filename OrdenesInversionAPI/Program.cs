using Microsoft.EntityFrameworkCore;
using OrdenesInversionAPI.Models;
using OrdenesInversionAPI.Services;
using Microsoft.AspNetCore.Authentication;
using OrdenesInversionAPI.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OrdenesInversionContext>(options =>
    options.UseInMemoryDatabase(databaseName: "TestDb")
);

builder.Services.AddScoped<IOrdenInversionService, OrdenInversionService>();

// Configuración de la autenticación básica
builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

var app = builder.Build();

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
