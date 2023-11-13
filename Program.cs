using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyecto.Models;



/******************* Constructores de la aplicacion ************************/
/* "Constructor" Es la estructura inicial builder proporciona un punto de entrada para  configurar y contruir la aplicacion */
var builder = WebApplication.CreateBuilder(args);

/* Configuracion a la base de datos */
builder.Services.AddDbContext<TareasContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionDB"));
});

/* Poporciona la configuracion de la aplicacion (ruta,middleware, entre otros)*/
var app = builder.Build();




/********************* Configuracion de servicios***************************/




/*************************  Mapeo de enrutamientos ****************************/

app.MapGet("/dbconexion", (HttpContext context, [FromServices] TareasContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("Base de datos en memoria: " + dbContext.Database.IsInMemory());
});


/*Get*/
app.MapGet("/api/tareas", async ([FromServices] TareasContext dbContext) =>
{
    return Results.Ok(dbContext.Tareas.Include(p => p.Categoria).Where(p => p.PrioridadTarea == proyecto.Models.Prioridad.Baja));
});


app.MapGet("/", () => "Hello World!");
app.Run();
