using System.ComponentModel.DataAnnotations;

namespace proyecto.Models;


public class Categoria{

    [Key]
    public Guid CategoriaId {get; set;}
    [Required]
    [MaxLength(150)]
    public string Nombre {get; set;}
    [Required]
    
    public string Descripcion { get; set; }
    [Required]
    public virtual ICollection<Tarea> Tareas { get; set; }
}
