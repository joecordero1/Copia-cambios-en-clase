using System.ComponentModel.DataAnnotations;

namespace Final_Cordero_Raura.Models
{
    public class Pelicula
    {
        [Key]
        public int IdPelicula { get; set; }
        public string? Nombre { get; set; }
        [DataType(DataType.Html)]
        public string? Descripcion { get; set; }
        public string? Genero { get; set; }
        public int anio { get; set; }
        public string? Poster { get; set; }

        public List<Resena>? Resenas { get; set; }
    }
}
