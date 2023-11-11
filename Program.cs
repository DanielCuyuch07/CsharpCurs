using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyecto.Models;

var builder = WebApplication.CreateBuilder(args);

/* 

    Configurar Entity Framework antes de definir rutas
    Util : Base  de datos en memoria 
 */


// builder.Services.AddDbContext<TareasContext>(options =>
// {
//     options.UseInMemoryDatabase("TareasDB");
// });

builder.Services.AddSqlServer<TareasContext>("Data Source=(local); Initial Catalog= TareasDb;  Integrated Security=True; TrustServerCertificate=True");



var app = builder.Build();

/* Mapeo de rutas */
app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", async (HttpContext context, [FromServices] TareasContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("Base de datos en memoria: " + dbContext.Database.IsInMemory());
});

app.Run();
