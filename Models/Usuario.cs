using System.ComponentModel.DataAnnotations;

namespace Final_Cordero_Raura.Models
{
    public class Usuario
    {
        [Key]
        public int Cedula { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }


        public List<Resena>? Resenas { get; set; }
    }
}
