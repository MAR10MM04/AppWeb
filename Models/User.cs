using System.ComponentModel.DataAnnotations;

namespace AppWeb.Models
{
    public class User
    {
        public int Id_User { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Upp { get; set; } // Corregido a "Upp"

        public ICollection<Lote> Lote { get; set; }
    }
}