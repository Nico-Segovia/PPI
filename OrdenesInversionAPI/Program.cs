using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using OrdenesInversionAPI.Handlers;
using OrdenesInversionAPI.Models;
using OrdenesInversionAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OrdenesInversionContext>(options =>
    options.UseInMemoryDatabase(databaseName: "TestDb")
);

builder.Services.AddScoped<IOrdenInversionService, OrdenesInversionAPI.Services.OrdenInversionService>();
builder.Services.AddScoped<IActivoFinancieroService, ActivoFinancieroService>();
// builder.Services.AddScoped<ITipoActivoService, TipoActivoService>(); // Eliminado ya que no se usa TipoActivo
builder.Services.AddScoped<IEstadoOrdenService, EstadoOrdenService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanCreateOrder", policy => policy.RequireClaim("CanCreateOrder", "true"));
    options.AddPolicy("CanUpdateOrder", policy => policy.RequireClaim("CanUpdateOrder", "true"));
    options.AddPolicy("CanDeleteOrder", policy => policy.RequireClaim("CanDeleteOrder", "true"));
});

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
