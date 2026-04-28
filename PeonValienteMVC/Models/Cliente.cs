using System.ComponentModel.DataAnnotations;

namespace PeonValienteMVC.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del cliente es un campo requerido.")]
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Poblacion { get; set; }
        public string? CodigoPostal { get; set; }
        public string? Nif { get; set; }
        public ICollection<Pedido>? Pedidos { get; set; }
    }
}
