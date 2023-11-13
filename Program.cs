using System.Runtime.Serialization;
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

/*Get*/
app.MapGet("/dbconexion", (HttpContext context, [FromServices] TareasContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("Base de datos en memoria: " + dbContext.Database.IsInMemory());
});


/*Get*/
app.MapGet("/api/tareas", async ([FromServices] TareasContext dbContext) =>
{
    return Results.Ok(dbContext.Tareas.Include(p => p.Categoria));
});

/*Post*/
app.MapPost("/api/tareas", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea) =>
{
    tarea.TareaId = Guid.NewGuid();
    tarea.FechaCreacion = DateTime.Now;
    await dbContext.AddAsync(tarea);

    await dbContext.SaveChangesAsync();
    return Results.Ok();
});

/*UPDATE*/
app.MapPut("/api/tareas", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea, [FromRoute] Guid id) =>
{
    var tareaActual = dbContext.Tareas.Find(id);


    if (tareaActual != null)
    {
        tareaActual.CategoriaId = tarea.CategoriaId;
        tareaActual.Titulo = tarea.Titulo;
        tareaActual.PrioridadTarea = tarea.PrioridadTarea;
        tareaActual.Descripcion = tarea.Descripcion;

        // Confirmar los datos 
        await dbContext.SaveChangesAsync();
        return Results.Ok();
    }
    return Results.NotFound();
});

/*Delete*/
app.MapDelete("/api/tareas/{id}", async ([FromServices] TareasContext dbContext, [FromRoute] Guid id) =>
{

    // Buscar el registro actual 
    var tareaActual = dbContext.Tareas.Find(id);
    if (tareaActual != null)
    {
        dbContext.Remove(tareaActual);
        await dbContext.SaveChangesAsync();
        return Results.Ok();
    }
    return Results.NotFound();
});


app.MapGet("/", () => "Hello World!");
app.Run();
