using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeonValienteMVC.Models
{
    public class Producto
    {
        public int Id { get; set; }
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "La descripción es un campo requerido.")]
        public string? Descripcion { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}")]
        [Column(TypeName = "decimal(18, 2)")]
        [Required(ErrorMessage = "El precio es un campo requerido")]
        [Display(Name = "Precio")]
               
        public decimal Precio { get; set; }
        
        public int? Stock { get; set; }
        public bool Escaparate { get; set; }
        public string? Imagen { get; set; }
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
        public ICollection<DetallePedido>? Detalles { get; set; }
    }
}
