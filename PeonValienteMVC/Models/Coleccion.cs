namespace PeonValienteMVC.Models
{
    public class Coleccion
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Prefijo { get; set; } = string.Empty;

        public ICollection<Idea> Ideas { get; set; }
            = new List<Idea>();
    }
}

