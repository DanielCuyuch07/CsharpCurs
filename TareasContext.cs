namespace proyecto.Models;
using Microsoft.EntityFrameworkCore;


public class TareasContext : DbContext
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Tarea> Tareas { get; set; }
    public TareasContext(DbContextOptions<TareasContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // Crear una coleccion que nos permite crear una data inicial
        List<Categoria> categoriasInit = new List<Categoria>();
        categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("f5122a67-dc4c-491c-ae01-4408e4841945"), Nombre = "Actividades pendientes", Peso = 20 });
        categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("f5122a67-dc4c-491c-ae01-4408e4841902"), Nombre = "Actividades Personales", Peso = 10 });


        // Configuraciones especificas en el modelo de datos 
        modelBuilder.Entity<Categoria>(Categoria =>
        {
            // EL Nombre de la tabla de la base de datos 
            Categoria.ToTable("Categoria");
            // Configuracion de la llave primaria
            Categoria.HasKey(p => p.CategoriaId);
            // Configuracion de las propiedades 
            Categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);
            Categoria.Property(p => p.Peso);
            // Descripcion de categoria de noes requeridad 
            Categoria.Property(p => p.Descripcion).IsRequired(false);

            Categoria.HasData(categoriasInit);
        });

        List<Tarea> tareasInit = new List<Tarea>();
        tareasInit.Add(new Tarea() { TareaId = Guid.Parse("f5122a67-dc4c-491c-ae01-4408e4841945"), CategoriaId = Guid.Parse("f5122a67-dc4c-491c-ae01-4408e4841945"), PrioridadTarea = Prioridad.Media, Titulo = "Pago de Servicios", FechaCreacion = DateTime.Now });
        tareasInit.Add(new Tarea() { TareaId = Guid.Parse("f5122a67-dc4c-491c-ae01-4408e4841900"), CategoriaId = Guid.Parse("f5122a67-dc4c-491c-ae01-4408e4841902"), PrioridadTarea = Prioridad.Baja, Titulo = "Pago de S", FechaCreacion = DateTime.Now });


        modelBuilder.Entity<Tarea>(tarea =>
        {
            tarea.ToTable("Tarea");
            tarea.HasKey(p => p.TareaId);
            tarea.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId);
            tarea.Property(p => p.Titulo).IsRequired().HasMaxLength(200);
            tarea.Property(p => p.Descripcion).IsRequired(false);
            tarea.Property(p => p.PrioridadTarea);
            tarea.Property(p => p.FechaCreacion);
            tarea.Ignore(p => p.Resumen);

            tarea.HasData(tareasInit);
        });
    }
}

