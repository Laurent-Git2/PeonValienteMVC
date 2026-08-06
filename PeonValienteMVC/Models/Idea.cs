using System.ComponentModel.DataAnnotations;

namespace PeonValienteMVC.Models
{
    public class Idea
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Código")]
        public string Codigo { get; set; } = string.Empty;
        // Exemple : BL001

        [Required]
        [StringLength(100)]
        [Display(Name = "Nombre interno")]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(300)]
        [Display(Name = "Frase en español")]
        public string? FraseEspanol { get; set; }

        [StringLength(300)]
        [Display(Name = "Frase en francés")]
        public string? FraseFrances { get; set; }

        [StringLength(1000)]
        [Display(Name = "Descripción visual")]
        public string? Descripcion { get; set; }

        [Display(Name = "Colección")]
        public int? ColeccionId { get; set; }

        public Coleccion? Coleccion { get; set; }

        [StringLength(50)]
        [Display(Name = "Tipo de producto")]
        public string? TipoProducto { get; set; }

        [Range(1, 10)]
        [Display(Name = "Potencial comercial")]
        public int Potencial { get; set; } = 5;

        [StringLength(30)]
        public string Estado { get; set; } = "Idea";

        [Display(Name = "Imagen terminada")]
        public bool ImagenTerminada { get; set; }

        [Display(Name = "Archivo POD terminado")]
        public bool ArchivoPodTerminado { get; set; }

        [Display(Name = "Mockup terminado")]
        public bool MockupTerminado { get; set; }

        [Display(Name = "Publicado en Etsy")]
        public bool PublicadoEtsy { get; set; }

        [Display(Name = "Publicado en Pinterest")]
        public bool PublicadoPinterest { get; set; }

        [StringLength(500)]
        [Display(Name = "Ruta de la imagen")]
        public string? RutaImagen { get; set; }

        [Display(Name = "Fecha de creación")]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [StringLength(1000)]
        public string? Notas { get; set; }
    }
}