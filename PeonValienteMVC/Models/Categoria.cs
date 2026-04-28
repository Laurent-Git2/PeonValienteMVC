using System.ComponentModel.DataAnnotations;

namespace PeonValienteMVC.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El Nombre es un campo requerido.")]
        public string? Descripcion { get; set; }
        
    }
}
