using System.ComponentModel.DataAnnotations;

namespace Final_Cordero_Raura.Models
{
    public class Resena
    {
        [Key]
        public int IdResena { get; set; }
        public string? Titulo { get; set; }
        //[Required]
        //[MaxLength]
        [DataType(DataType.Html)]
        public string? Texto { get; set; }

        public Pelicula? Pelicula { get; set; }
        public int IdPelicula { get; set; }
    }
}
