namespace proyecto.Models;
using Microsoft.EntityFrameworkCore;


public class TareasContext: DbContext{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Tarea> Tareas { get; set; }
    public TareasContext(DbContextOptions<TareasContext> options): base(options){ }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        // Configuraciones especificas en el modelo de datos 
        modelBuilder.Entity<Categoria>(Categoria =>
        {
            // EL Nombre de la tabla de la base de datos 
            Categoria.ToTable("Categoria");
            // Configuracion de la llave primaria
            Categoria.HasKey(p => p.CategoriaId);
            // Configuracion de las propiedades 
            Categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);

            Categoria.Property(p => p.Descripcion);
        });
    }
}

