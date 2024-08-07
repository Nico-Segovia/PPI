using Microsoft.EntityFrameworkCore;
using OrdenesInversionAPI.Models;
using OrdenesInversionAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OrdenesInversionContext>(options =>
    options.UseInMemoryDatabase(databaseName: "TestDb")
);

// Registro del servicio OrdenInversionService
builder.Services.AddScoped<IOrdenInversionService, OrdenInversionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
